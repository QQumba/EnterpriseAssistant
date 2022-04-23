using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.API;

public static class UserServiceIServiceCollectionExtensions
{
    public static void AddUserService(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining(typeof(UserServiceIServiceCollectionExtensions)));

        services.AddMediatR(typeof(UserServiceIServiceCollectionExtensions));
    }
}