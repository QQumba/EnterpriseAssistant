using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;
using UserService.Contract.ViewModels;

namespace EnterpriseService.API.Commands;

public class CreateEnterpriseUser : IRequest<OneOf<UserViewModel>>
{
    public CreateEnterpriseUser(UserCreateViewModel model)
    {
        Model = model;
    }

    public UserCreateViewModel Model { get; }
}

public class CreateEnterpriseUserHandler : IRequestHandler<CreateEnterpriseUser, OneOf<UserViewModel>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseUserHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }
    
    public async Task<OneOf<UserViewModel>> Handle(CreateEnterpriseUser request, CancellationToken cancellationToken)
    {
        var userToCreate = request.Model.Adapt<User>();
        userToCreate.Salt = "default_salt";
        var user = _db.Users.Add(userToCreate).Entity;

        await _db.SaveChangesAsync(cancellationToken);
        return user.Adapt<UserViewModel>();
    }
}