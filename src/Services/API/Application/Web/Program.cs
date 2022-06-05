using System.Text;
using DepartmentService.API;
using EnterpriseAssistant.Application;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.Web;
using EnterpriseAssistant.Web.Filters;
using EnterpriseAssistant.Web.Helpers;
using EnterpriseAssistant.Web.Middleware;
using EnterpriseService.API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

    services.AddJwtAuthentication(configuration);

    services.AddSwaggerGen(o =>
    {
        o.EnableAnnotations();
        o.TagActionsBy(api =>
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
        o.AddOAuthAuthentication(configuration);

        o.OperationFilter<AuthorizeCheckOperationFilter>();
        o.DocInclusionPredicate((name, api) => true);
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
        app.UseSwaggerUI(o =>
        {
            o.EnablePersistAuthorization();
            o.OAuthClientId("ea.swagger");
            o.OAuthClientSecret("ea.secret");
            o.OAuthScopes("ea");
            o.OAuthUsePkce();
        });
    }

    // todo: move to config
    app.UseCors(b =>
        b.WithOrigins("https://localhost:5004", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEnterpriseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app)
{
    app.MapControllers();
}

void ConfigureLogging(WebApplicationBuilder app, IConfiguration configuration)
{
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    app.Host.UseSerilog(logger);
}