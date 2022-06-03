using DepartmentService.API;
using EnterpriseAssistant.Application;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.Web.Filters;
using EnterpriseService.API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Controllers;
using ProjectService.API;
using Serilog;
using TaskTrackingService.API;
using UserService.API;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration);
ConfigureLogging(builder, builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);

var webApplication = builder.Build();

ConfigureMiddleware(webApplication, webApplication.Environment);
ConfigureEndpoints(webApplication);

webApplication.Run();

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
            o.Scope.Add("ea");
        });

    services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
        c.TagActionsBy(api =>
        {
            if (api.GroupName != null)
            {
                return new[] {api.GroupName};
            }

            if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return new[] {controllerActionDescriptor.ControllerName};
            }

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });
        c.DocInclusionPredicate((name, api) => true);
    });

    services.AddControllers(c =>
    {
        c.Filters.Add<AuditActionFilter>();
    });
    
    services.AddDataAccess(configuration);
    services.AddApplication();

    services.AddDepartmentService();
    services.AddEnterpriseService();
    services.AddProjectService();
    services.AddTaskTrackingService();
    services.AddUserService();
}

void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    // todo: move to config
    app.UseCors(b => b.WithOrigins("https://localhost:5004", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app)
{
    app.MapControllers().RequireAuthorization();
}

void ConfigureLogging(WebApplicationBuilder app, IConfiguration configuration)
{
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    app.Host.UseSerilog(logger);
}