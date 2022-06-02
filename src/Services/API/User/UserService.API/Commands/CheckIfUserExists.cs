using EnterpriseAssistant.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.API.Helpers;

namespace UserService.API.Commands;

public class CheckIfUserExists : IRequest<bool>
{
    public CheckIfUserExists(string email)
    {
        Email = email;
    }

    public string Email { get; }
}

public class CheckIfUserExistsHandler : IRequestHandler<CheckIfUserExists, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CheckIfUserExistsHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(CheckIfUserExists request, CancellationToken cancellationToken)
    {
        return await _db.Users.IsEmailTaken(request.Email, cancellationToken);
    }
}