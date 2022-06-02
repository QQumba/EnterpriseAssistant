using System.Threading;
using System.Threading.Tasks;
using ProjectService.Contract.DataTransfer;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using ProjectService.Contract.DataTransfer;
using Mapster;
using MediatR;
using OneOf;

namespace ProjectService.API.Commands;

public class CreateProject : IRequest<OneOf<ProjectCreateDto>>
{
    public CreateProject(ProjectCreateDto projectCreate)
    {
        ProjectCreate = projectCreate;
    }

    public ProjectCreateDto ProjectCreate { get;}
}
