using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace EnterpriseAssistant.Identity.DataAccess
{
    public static class AddDataAccessDiExtension
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserRepository>();
            services.AddTransient<IDbConnection>(
                db =>
                {
                    var connectionString = configuration.GetConnectionString("Npgsql");
                    return new NpgsqlConnection(connectionString);
                });
        }
    }
}