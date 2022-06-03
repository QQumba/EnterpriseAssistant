using ProjectService.Contract.DataTransfer;
using EnterpriseAssistant.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace ProjectService.API.Commands;

internal class DeleteProject : IRequest<OneOf<IEnumerable<ProjectDeleteDto>, NotFound>>
{
    public DeleteProject (long id)
    {
        Id = id;
    }

    public long Id { get; set; }

}

public class ProjectDeleteHandler :IRequestHandler<DeleteProject, OneOf<IEnumerable<ProjectDeleteDto>, NotFound>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public ProjectDeleteHandler (EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    Task<OneOf<IEnumerable<ProjectDeleteDto>, NotFound>> IRequestHandler<DeleteProject, OneOf<IEnumerable<ProjectDeleteDto>, NotFound>>.Handle(DeleteProject request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}