using EnterpriseAssistant.Application.Shared;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseService.API;

public static class EnterpriseServiceIServiceCollectionExtensions
{
    public static void AddEnterpriseService(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining(typeof(EnterpriseServiceIServiceCollectionExtensions),
                    filter => true
                ));

        services.AddMediatR(typeof(EnterpriseServiceIServiceCollectionExtensions));
    }
}