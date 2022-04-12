using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using UserService.Contract.ViewModels;

namespace EnterpriseService.API.Commands;

public class CreateUser : IRequest<OneOf<UserViewModel>>
{
    public CreateUser(UserCreateViewModel model)
    {
        Model = model;
    }

    public UserCreateViewModel Model { get; }
}

public class CreateEnterpriseUserHandler : IRequestHandler<CreateUser, OneOf<UserViewModel>>
{
    public Task<OneOf<UserViewModel>> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}