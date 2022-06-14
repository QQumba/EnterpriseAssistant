using ProjectService.Contract.DataTransfer;
using EnterpriseAssistant.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace ProjectService.API.Commands;

public class GetProjectById : IRequest<OneOf<IEnumerable<ProjectDto>, NotFound>>
{
    public GetProjectById(long id)
    {
        Id = id;
    }

    public long Id { get; }

}
