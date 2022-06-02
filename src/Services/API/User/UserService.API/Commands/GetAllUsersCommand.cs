using EnterpriseAssistant.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Contract.DataTransfer;

namespace UserService.API.Commands;

public class GetAllUsersCommand
    : IRequest<IEnumerable<UserDto>>
{
}

public class GetAllUsersCommandHandler
    : IRequestHandler<GetAllUsersCommand, IEnumerable<UserDto>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetAllUsersCommandHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersCommand request,
        CancellationToken cancellationToken)
    {
        return (await _db.Users.ToListAsync(cancellationToken)).Adapt<IEnumerable<UserDto>>();
    }
}