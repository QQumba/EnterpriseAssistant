using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseAssistant.DataAccess;

public static class DataAccessDependencyInjectionExtensions
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EnterpriseAssistantDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("Npgsql")));
    }
}