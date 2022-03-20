using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentService.API;

public static class DepartmentServiceIServiceCollectionExtensions
{
    public static void AddDepartmentService(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining(typeof(DepartmentServiceIServiceCollectionExtensions)));

        services.AddMediatR(typeof(DepartmentServiceIServiceCollectionExtensions));
    }
}