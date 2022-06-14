using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.DataAccess.Entities;

// todo: add configuration
// todo: might should be renamed
[Table("task")]
public class Task : BaseEntity.WithId<long>
{
    [Column("title")]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("user_id")]
    public long? UserId { get; set; }

    public User? User { get; set; }

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;
    public Enterprise? Enterprise { get; set; }

    [Column("project_id")]
    public long ProjectId { get; set; }
    public Project? Project { get; set; }
}