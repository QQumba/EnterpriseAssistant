using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.DataAccess.Entities;

// todo: add configuration
// todo: might should be renamed
[Table("project")]
public class Project : BaseEntity.WithId<long>
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

}