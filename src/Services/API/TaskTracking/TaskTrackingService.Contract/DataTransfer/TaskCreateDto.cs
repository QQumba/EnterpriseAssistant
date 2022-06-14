namespace TaskTrackingService.Contract.DataTransfer;

public class TaskCreateDto
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public long ProjectId { get; set; }
}