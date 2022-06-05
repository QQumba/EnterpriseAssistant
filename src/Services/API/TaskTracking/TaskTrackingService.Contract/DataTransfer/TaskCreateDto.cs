namespace TaskTrackingService.Contract.DataTransfer;
    public class TaskCreateDto
    {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public long  UserId { get; set; }
    public string EnterpriseId { get; set; } = null!;
    public long ProjectId { get; set; }

    }
