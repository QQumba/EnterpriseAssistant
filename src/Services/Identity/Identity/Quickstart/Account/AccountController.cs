// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EnterpriseAssistant.Identity.DataTransfer;
using EnterpriseAssistant.Identity.DTOs;
using EnterpriseAssistant.Identity.Services;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAssistant.Identity.Quickstart.Account
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    //[SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            UserService userService,
            AuthService authService,
            IMapper mapper)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (button == "signup")
            {
                return RedirectToAction("SignUp", new { returnUrl = model.ReturnUrl });
            }

            if (ModelState.IsValid)
            {
                // validate username/password against in-memory store
                if (await _authService.IsUserValidAsync(new LoginDto
                        { Email = model.Email, Password = model.Password }))
                {
                    var user = await _userService.GetUserByEmailAsync(model.Email);

                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Email, user.Email, user.FirstName,
                        clientId: context?.Client.ClientId));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration),
                        };
                    }

                    ;

                    // issue authentication cookie with subject ID and username
                    var identityServerUser = new IdentityServerUser(user.Email)
                    {
                        AdditionalClaims =
                        {
                            new Claim("user_id", user.Id.ToString()),
                            new Claim("email", user.Email),
                            new Claim("first_name", user.FirstName),
                        }
                    };
                    
                    if(user.EnterpriseIds is not null)
                    {
                        identityServerUser.AdditionalClaims.Add(new Claim("enterprise_ids", user.EnterpriseIds));
                    }

                    await HttpContext.SignInAsync(identityServerUser, props);

                    if (context != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }

                    // user might have clicked on a malicious link - should be logged
                    throw new Exception("invalid return URL");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials",
                    clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return Redirect(vm.PostLogoutRedirectUri);
        }

        [HttpGet]
        public async Task<IActionResult> SignUp(string returnUrl)
        {
            var model = new SignupViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model, string button)
        {
            if (button == "login")
            {
                return RedirectToAction("Login", new { returnUrl = model.ReturnUrl });
            }

            if (ModelState.IsValid == false)
            {
                return View(await BuildLoginViewModelAsync(model) as SignupViewModel);
            }

            if (model.Password != model.PasswordConfirmation)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");
                return View(await BuildLoginViewModelAsync(model) as SignupViewModel);
            }

            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser is not null)
            {
                ModelState.AddModelError(String.Empty, "User with this email already exists");
                return View(await BuildLoginViewModelAsync(model) as SignupViewModel);
            }

            var userToCreate = _mapper.Map<UserCreateDto>(model);
            var user = await _userService.CreateUserAsync(userToCreate);
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            
            if (await _authService.IsUserValidAsync(new LoginDto { Email = model.Email, Password = model.Password }))
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.Email, user.Email, user.FirstName,
                    clientId: context?.Client.ClientId));

                // only set explicit expiration here if user chooses "remember me". 
                // otherwise we rely upon expiration configured in cookie middleware.
                AuthenticationProperties props = null;
                if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration),
                    };
                }

                // issue authentication cookie with subject ID and username
                var identityServerUser = new IdentityServerUser(user.Email)
                {
                    AdditionalClaims =
                    {
                        new Claim("user_id", user.Id.ToString()),
                        new Claim("email", user.Email),
                        new Claim("first_name", user.FirstName),
                    }
                };
                
                if(user.EnterpriseIds is not null)
                {
                    identityServerUser.AdditionalClaims.Add(new Claim("enterprise_ids", user.EnterpriseIds));
                }

                await HttpContext.SignInAsync(identityServerUser, props);

                if (context != null)
                {
                    return Redirect(model.ReturnUrl);
                }

                // request for a local page
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect("~/");
                }

                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials",
                clientId: context?.Client.ClientId));
            ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);

            var vm = await BuildLoginViewModelAsync(model) as SignupViewModel;
            return View(vm);
        }

        private async Task<LoginInputModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            return new LoginInputModel
            {
                ReturnUrl = returnUrl,
                Email = context?.LoginHint,
            };
        }

        private async Task<LoginInputModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Email = model.Email;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}