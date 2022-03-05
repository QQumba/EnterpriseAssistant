﻿using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using MediatR;
using OneOf;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.Commands;

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