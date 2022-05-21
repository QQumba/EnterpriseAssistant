using EnterpriseAssistant.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Contract.ViewModels;

namespace UserService.API.Commands;

public class GetAllManagedUsersCommand
    : IRequest<IEnumerable<ManagedUserViewModel>>
{
}

public class GetAllManagedUsersCommandHandler
    : IRequestHandler<GetAllManagedUsersCommand, IEnumerable<ManagedUserViewModel>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetAllManagedUsersCommandHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ManagedUserViewModel>> Handle(GetAllManagedUsersCommand request,
        CancellationToken cancellationToken)
    {
        return (await _db.ManagedUsers.ToListAsync(cancellationToken)).Adapt<IEnumerable<ManagedUserViewModel>>();
    }
}