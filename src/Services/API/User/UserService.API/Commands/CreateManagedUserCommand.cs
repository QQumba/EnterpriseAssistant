using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;
using UserService.API.Helpers;
using UserService.API.OneOfResponses;
using UserService.Contract.DataTransfer;

namespace UserService.API.Commands;

public class CreateManagedUserCommand : IRequest<OneOf<ManagedUserDto,EmailTakenError>>
{
    public CreateManagedUserCommand(ManagedUserCreateDto model)
    {
        Model = model;
    }

    public ManagedUserCreateDto Model { get; }
}

public class CreateManagedUserCommandHandler
    : IRequestHandler<CreateManagedUserCommand,OneOf<ManagedUserDto,EmailTakenError>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateManagedUserCommandHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<ManagedUserDto,EmailTakenError>> Handle(CreateManagedUserCommand request,
        CancellationToken cancellationToken)
    {
        if (await _db.ManagedUsers.IsEmailTaken(request.Model.Email,cancellationToken))
        {
            return new EmailTakenError(request.Model.Email);
        }

        var userToCreate = request.Model.Adapt<ManagedUser>();
        var createdUser = _db.ManagedUsers.Add(userToCreate).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        
        // TODO: trigger email verification process
        
        return createdUser.Adapt<ManagedUserDto>();
    }
}