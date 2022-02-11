using EnterpriseAssistant.Application;
using EnterpriseAssistant.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Controllers;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app, app.Environment);
ConfigureEndpoints(app);

app.Run();

void ConfigureConfiguration(IConfigurationBuilder configuration)
{
    configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", true);
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();

    // todo: move auth values to config
    services.AddAuthentication(o =>
        {
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = "oidc";
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddOpenIdConnect("oidc", o =>
        {
            o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.Authority = "https://localhost:5004";
            o.ClientId = "ea.api";
            o.ClientSecret = "ea.secret";
            o.ResponseType = "code";

            o.SaveTokens = true;
            o.GetClaimsFromUserInfoEndpoint = true;
            o.ClaimActions.MapUniqueJsonKey("login", "login");
            o.Scope.Add("openid");
            o.Scope.Add("profile");
        });

    services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
        c.TagActionsBy(api =>
        {
            if (api.GroupName != null)
            {
                return new[] { api.GroupName };
            }

            if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return new[] { controllerActionDescriptor.ControllerName };
            }

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });
        c.DocInclusionPredicate((name, api) => true);
    });

    services.AddDataAccess(configuration);
    services.AddApplication();
}

void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(b => b.WithOrigins("https://localhost:5004").AllowAnyHeader().AllowAnyMethod());
    app.UseHttpsRedirection();
    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app)
{
    app.MapControllers();
}