using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseService.API.Commands;

public class GetEnterpriseNameAvailability : IRequest<bool>
{
    public GetEnterpriseNameAvailability(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

public class GetEnterpriseNameAvailabilityHandler : IRequestHandler<GetEnterpriseNameAvailability, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetEnterpriseNameAvailabilityHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(GetEnterpriseNameAvailability request, CancellationToken cancellationToken)
    {
        return await _db.Enterprises.AnyAsync(e => e.Name.Equals(request.Name), cancellationToken);
    }
}