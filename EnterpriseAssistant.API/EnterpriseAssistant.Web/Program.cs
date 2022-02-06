using EnterpriseAssistant.DataAccess;

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
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDataAccess(configuration);
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