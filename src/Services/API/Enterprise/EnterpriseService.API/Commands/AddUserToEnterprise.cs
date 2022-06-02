using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Errors;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using EnterpriseService.API.Helpers;
using EnterpriseService.API.OneOfResponses;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using UserService.Contract.DataTransfer;

namespace EnterpriseService.API.Commands;

public class AddUserToEnterprise : IRequest<OneOf<UserDto, INotFoundError, IBadRequestError>>
{
    public AddUserToEnterprise(UserCreateDto model, string enterpriseId)
    {
        Model = model;
        EnterpriseId = enterpriseId;
    }

    public UserCreateDto Model { get; }

    public string EnterpriseId { get; }
}

public class AddUserToEnterpriseHandler
    : IRequestHandler<AddUserToEnterprise, OneOf<UserDto, INotFoundError, IBadRequestError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public AddUserToEnterpriseHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<UserDto, INotFoundError, IBadRequestError>> Handle(
        AddUserToEnterprise request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}