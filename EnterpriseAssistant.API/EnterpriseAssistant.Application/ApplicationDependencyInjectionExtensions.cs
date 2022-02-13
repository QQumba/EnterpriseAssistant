using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseAssistant.Application;

public static class ApplicationDependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>());

        services.AddMediatR(typeof(IApplicationAssemblyMarker));
    }
}