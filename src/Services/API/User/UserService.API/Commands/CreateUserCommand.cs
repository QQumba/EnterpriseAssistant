using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;
using UserService.API.Helpers;
using UserService.API.OneOfResponses;
using UserService.Contract.DataTransfer;

namespace UserService.API.Commands;

public class CreateUserCommand : IRequest<OneOf<UserDto,EmailTakenError>>
{
    public CreateUserCommand(UserCreateDto model)
    {
        Model = model;
    }

    public UserCreateDto Model { get; }
}

public class CreateManagedUserCommandHandler 
    : IRequestHandler<CreateUserCommand,OneOf<UserDto,EmailTakenError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateManagedUserCommandHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<UserDto,EmailTakenError>> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        if (await _db.Users.IsEmailTaken(request.Model.Email,cancellationToken))
        {
            return new EmailTakenError(request.Model.Email);
        }

        var userToCreate = request.Model.Adapt<User>();
        var createdUser = _db.Users.Add(userToCreate).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        
        // TODO: trigger email verification process
        
        return createdUser.Adapt<UserDto>();
    }
}