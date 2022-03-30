using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseService.API.Commands;

public class GetEnterpriseIdAvailability : IRequest<bool>
{
    public GetEnterpriseIdAvailability(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class GetEnterpriseIdAvailabilityHandler : IRequestHandler<GetEnterpriseIdAvailability, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetEnterpriseIdAvailabilityHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(GetEnterpriseIdAvailability request, CancellationToken cancellationToken)
    {
        return !await _db.Enterprises.AnyAsync(e => e.Id.Equals(request.Id), cancellationToken);
    }
}