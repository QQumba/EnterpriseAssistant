using EnterpriseAssistant.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Contract.DataTransfer;

namespace UserService.API.Commands;

public class GetAllManagedUsersCommand
    : IRequest<IEnumerable<ManagedUserDto>>
{
}

public class GetAllManagedUsersCommandHandler
    : IRequestHandler<GetAllManagedUsersCommand, IEnumerable<ManagedUserDto>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetAllManagedUsersCommandHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ManagedUserDto>> Handle(GetAllManagedUsersCommand request,
        CancellationToken cancellationToken)
    {
        return (await _db.ManagedUsers.ToListAsync(cancellationToken)).Adapt<IEnumerable<ManagedUserDto>>();
    }
}