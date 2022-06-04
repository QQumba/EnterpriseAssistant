using EnterpriseAssistant.Application.Shared;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectService.API;

public static class ProjectServiceIServiceCollectionExtensions
{
    public static void AddProjectService(this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining(typeof(ProjectServiceIServiceCollectionExtensions)));
        services.AddMediatR(typeof(ProjectServiceIServiceCollectionExtensions));
    }



}