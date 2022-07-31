using System.Text.Json.Serialization;
using EnterpriseAssistant.Application;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.Web;
using EnterpriseAssistant.Web.Features.Tasks.Repositories;
using EnterpriseAssistant.Web.Features.Tasks.Services;
using EnterpriseAssistant.Web.Filters;
using EnterpriseAssistant.Web.Helpers.Security;
using EnterpriseAssistant.Web.Middleware;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

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

    services.AddAuthorization(o =>
    {
        o.AddPolicy("EnterpriseUser", p => p.RequireClaim(ClaimUtilities.EnterpriseId));
    });
    services.AddControllers(c =>
    {
        c.Filters.Add<AuditActionFilter>();
    })
        .AddJsonOptions(o =>
        {
            var enumConverter = new JsonStringEnumConverter();
            o.JsonSerializerOptions.Converters.Add(enumConverter);
        });

    services.AddDataAccess(configuration);
    services.AddApplication();

    services.AddScoped<ITaskRepository, TaskRepository>();
    services.AddScoped<ITaskService, TaskService>();
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
    app.UseEnterpriseAuthorization();
    app.UseAuthorization();
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