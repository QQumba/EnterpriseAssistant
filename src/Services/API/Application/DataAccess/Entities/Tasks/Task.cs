using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.DataAccess.Entities.Tasks;

[Table("task")]
public class Task : BaseEntity
{
    [Column("task_id")]
    public long TaskId { get; set; }

    [Column("title")]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("status")]
    public TaskStatus Status { get; set; }

    public ICollection<User>? AssignedUsers { get; set; }

    public ICollection<Tag>? Tags { get; set; }

    public ICollection<Task>? UpstreamTasks { get; set; }

    public ICollection<Task>? DownstreamTasks { get; set; }

    [Column("created_by_user_id")]
    public long CreatedByUserId { get; set; }

    public User CreatedByUser { get; set; } = null!;

    [Column("closed_by_user_id")]
    public long? ClosedByUserId { get; set; }

    public User? ClosedByUser { get; set; }

    [Column("is_task_group")]
    public bool IsTaskGroup { get; set; }

    [Column("task_group_id")]
    public long? TaskGroupId { get; set; }

    [Column("estimated_hours")]
    public double? EstimatedHours { get; set; }

    [Column("effort_hours")]
    public double? EffortHours { get; set; }

    // [Column("enterprise_id")]
    // public long EnterpriseId { get; set; }
    //
    // [Column("project_id")]
    // public long ProjectId { get; set; }
}

[Table("tag")]
public class Tag
{
    public long TagId { get; set; }

    public string Name { get; set; } = null!;
}

public enum TaskStatus
{
    None = 0,
    Committed,
    InProgress,
    Completed,
    Rejected
}