using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.API.Helpers;

namespace UserService.API.Commands;

public class GetManagedUserEmailAvailability : IRequest<bool>
{
    public GetManagedUserEmailAvailability(string email)
    {
        Email = email;
    }

    public string Email { get; }
}

public class GetManagedUserEmailAvailabilityHandler : IRequestHandler<GetManagedUserEmailAvailability, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetManagedUserEmailAvailabilityHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(GetManagedUserEmailAvailability request, CancellationToken cancellationToken)
    {
        return !await _db.ManagedUsers.IsEmailTaken(request.Email, cancellationToken);
    }
}