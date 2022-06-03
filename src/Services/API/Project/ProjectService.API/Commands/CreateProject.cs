using ProjectService.Contract.DataTransfer;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
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

public class CreateProjectHandler : IRequestHandler<CreateProject, OneOf<ProjectCreateDto>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateProjectHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<ProjectCreateDto>> Handle(CreateProject request, CancellationToken cancellationToken)
    {
        var project = _db.Projects.Add(request.ProjectCreate.Adapt<Project>()).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        return project.Adapt<ProjectCreateDto>();
    }
}