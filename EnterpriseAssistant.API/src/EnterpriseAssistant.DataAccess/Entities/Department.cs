using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("department")]
public class Department : BaseEntity.WithId<long>
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("parent_department_id")]
    public long? ParentDepartmentId { get; set; }

    public Department? ParentDepartment { get; set; }

    // TODO: check if no child departments exist, is this null or empty?
    public ICollection<Department>? ChildDepartments { get; set; }

    [Column("department_type")]
    public DepartmentType DepartmentType { get; set; } = DepartmentType.Default;

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;

    public Enterprise? Enterprise { get; set; }
}