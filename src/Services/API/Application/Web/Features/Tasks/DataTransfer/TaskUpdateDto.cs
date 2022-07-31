namespace EnterpriseAssistant.Web.Features.Tasks.DataTransfer;

public class TaskUpdateDto
{
    public long TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public double? EstimatedHours { get; set; }

    public double? EffortHours { get; set; }
}