using System.Threading;
using System.Threading.Tasks;
using EnterpriseAssistant.DataAccess;
using EnterpriseService.API.Helpers;
using MediatR;

namespace EnterpriseService.API.Commands;

public class GetEnterpriseUserExistence : IRequest<bool>
{
    public GetEnterpriseUserExistence(string enterpriseId, string userLogin)
    {
        EnterpriseId = enterpriseId;
        UserLogin = userLogin;
    }

    public string EnterpriseId { get; }

    public string UserLogin { get; set; }
}

public class GetEnterpriseUserExistenceHandler : IRequestHandler<GetEnterpriseUserExistence, bool>
{
    private readonly EnterpriseAssistantDbContext _db;

    public GetEnterpriseUserExistenceHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(GetEnterpriseUserExistence request, CancellationToken cancellationToken)
    {
        return await _db.EnterpriseUsers.IsEnterpriseUserExists(request.EnterpriseId, request.UserLogin, cancellationToken);
    }
}