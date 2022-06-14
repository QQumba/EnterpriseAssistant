using EnterpriseAssistant.Application.Shared;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTrackingService.API;

public static class TaskTrackingServiceIServiceCollectionExtensions
{
    public static void AddTaskTrackingService(this IServiceCollection services)
    {
        services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining(typeof(TaskTrackingServiceIServiceCollectionExtensions)));

        services.AddMediatR(typeof(TaskTrackingServiceIServiceCollectionExtensions));
    }
}