using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EnterpriseAssistant.Web.Helpers;

public static class SwaggerAuthenticationExtensions
{
    public static void AddOAuthAuthentication(this SwaggerGenOptions options, IConfiguration configuration)
    {
        options.AddSecurityDefinition("OAuth", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    // todo: move to config
                    AuthorizationUrl = new Uri("https://localhost:5004/connect/authorize"),
                    TokenUrl = new Uri("https://localhost:5004/connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        {
                            "ea", "full-access"
                        }
                    }
                }
            }
        });
        
        options.AddSecurityDefinition("Enterprise", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "auth-enterprise",
            Type = SecuritySchemeType.ApiKey,
        });
    }
}