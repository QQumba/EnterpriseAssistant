using System.ComponentModel.DataAnnotations;
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

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;
    public Enterprise? Enterprise { get; set; }

    [Column("department_id")]
    public long DepartmentId  { get; set; }
    public Department? Department { get; set; }
}