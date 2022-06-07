using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess;
using EnterpriseService.API.Helpers;
using MediatR;

namespace EnterpriseService.API.Commands;

public class CheckIfEnterpriseUserExists : IRequest<bool>
{
    public CheckIfEnterpriseUserExists(string enterpriseId, string userLogin)
    {
        EnterpriseId = enterpriseId;
        UserLogin = userLogin;
    }

    public string EnterpriseId { get; }

    public string UserLogin { get; set; }
}

public class CheckIfEnterpriseUserExistsHandler : IRequestHandler<CheckIfEnterpriseUserExists, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CheckIfEnterpriseUserExistsHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(CheckIfEnterpriseUserExists request, CancellationToken cancellationToken)
    {
        return await _db.EnterpriseUsers.IsEnterpriseUserExists(request.EnterpriseId, request.UserLogin, cancellationToken);
    }
}