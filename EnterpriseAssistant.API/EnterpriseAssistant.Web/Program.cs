using EnterpriseAssistant.Application;
using EnterpriseAssistant.DataAccess;
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
    configuration.AddJsonFile($"launchSettings.{Environment.MachineName}.json", true);
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // services.AddControllers().AddApplicationPart(typeof(IApplicationAssemblyMarker).Assembly);
    services.AddEndpointsApiExplorer();
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

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app)
{
    app.MapControllers();
}