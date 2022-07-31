using TaskStatus = EnterpriseAssistant.DataAccess.Entities.Tasks.TaskStatus;

namespace EnterpriseAssistant.Web.Features.Tasks.DataTransfer;

public class TaskDto : TaskCreateDto
{
    public long TaskId { get; set; }
}

public class TaskCreateDto
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
    
    public TaskStatus Status { get; set; }

    public double? EstimatedHours { get; set; }
    
    public double? EffortHours { get; set; }
}