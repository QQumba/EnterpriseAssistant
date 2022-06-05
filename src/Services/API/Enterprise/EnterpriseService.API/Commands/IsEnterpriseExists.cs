using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseService.API.Commands;

public class IsEnterpriseExists : IRequest<bool>
{
    public IsEnterpriseExists(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class IsEnterpriseExistsHandler : IRequestHandler<IsEnterpriseExists, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public IsEnterpriseExistsHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(IsEnterpriseExists request, CancellationToken cancellationToken)
    {
        return await _db.Enterprises.AnyAsync(e => e.Id.Equals(request.Id), cancellationToken);
    }
}