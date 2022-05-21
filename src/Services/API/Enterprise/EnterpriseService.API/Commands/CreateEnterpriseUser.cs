using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using EnterpriseService.API.Helpers;
using EnterpriseService.API.OneOfResponses;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using UserService.Contract.ViewModels;

namespace EnterpriseService.API.Commands;

public class CreateEnterpriseUser : IRequest<OneOf<UserViewModel, EnterpriseNotFoundError, EnterpriseUserAlreadyExists>>
{
    public CreateEnterpriseUser(UserCreateViewModel model, string enterpriseId)
    {
        Model = model;
        EnterpriseId = enterpriseId;
    }

    public UserCreateViewModel Model { get; }

    public string EnterpriseId { get; }
}

public class CreateEnterpriseUserHandler
    : IRequestHandler<CreateEnterpriseUser, OneOf<UserViewModel, EnterpriseNotFoundError, EnterpriseUserAlreadyExists>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateEnterpriseUserHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<UserViewModel, EnterpriseNotFoundError, EnterpriseUserAlreadyExists>> Handle(
        CreateEnterpriseUser request,
        CancellationToken cancellationToken)
    {
        var rootDepartment =
            await _db.Departments.FirstOrDefaultAsync(d =>
                d.EnterpriseId == request.EnterpriseId && d.DepartmentType == DepartmentType.Root, cancellationToken);

        if (rootDepartment is null)
        {
            return new EnterpriseNotFoundError(request.EnterpriseId);
        }

        var userAlreadyExists =
            await _db.Users.IsEnterpriseUserExists(request.EnterpriseId, request.Model.Login, cancellationToken);

        if (userAlreadyExists)
        {
            return new EnterpriseUserAlreadyExists(request.EnterpriseId, request.Model.Login);
        }

        var userToCreate = request.Model.Adapt<User>();
        userToCreate.EnterpriseId = request.EnterpriseId;
        userToCreate.Salt = "default_salt";
        var user = _db.Users.Add(userToCreate).Entity;

        var departmentUser = new DepartmentUser
        {
            Department = rootDepartment,
            User = user,
            DepartmentUserRole = DepartmentUserRole.User,
            EnterpriseId = request.EnterpriseId
        };

        _db.DepartmentUsers.Add(departmentUser);

        await _db.SaveChangesAsync(cancellationToken);
        return user.Adapt<UserViewModel>();
    }
}