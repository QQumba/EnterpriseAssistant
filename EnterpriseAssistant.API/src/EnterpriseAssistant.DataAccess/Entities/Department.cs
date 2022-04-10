using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

public class Department : BaseEntity.WithId<long>
{
    public string Name { get; set; } = null!;

    public long? ParentDepartmentId { get; set; }

    public Department? ParentDepartment { get; set; }

    // TODO: check if no child departments exist, is this null or empty?
    public ICollection<Department>? ChildDepartments { get; set; }

    public DepartmentType DepartmentType { get; set; } = DepartmentType.Default;

    public string EnterpriseId { get; set; } = null!;

    public Enterprise? Enterprise { get; set; }
}