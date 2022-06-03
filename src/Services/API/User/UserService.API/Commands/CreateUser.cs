using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;
using UserService.API.Helpers;
using UserService.API.OneOfResponses;
using UserService.Contract.DataTransfer;

namespace UserService.API.Commands;

public class CreateUser : IRequest<OneOf<UserDto,EmailTakenError>>
{
    public CreateUser(UserCreateDto model)
    {
        Model = model;
    }

    public UserCreateDto Model { get; }
}

public class CreateUserHandler 
    : IRequestHandler<CreateUser,OneOf<UserDto,EmailTakenError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateUserHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<UserDto,EmailTakenError>> Handle(CreateUser request,
        CancellationToken cancellationToken)
    {
        if (await _db.Users.IsEmailTaken(request.Model.Email,cancellationToken))
        {
            return new EmailTakenError(request.Model.Email);
        }

        var user = request.Model.Adapt<User>();
        user.Salt = "test_salt";
        var createdUser = _db.Users.Add(user).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        
        // TODO: trigger email verification process
        
        return createdUser.Adapt<UserDto>();
    }
}