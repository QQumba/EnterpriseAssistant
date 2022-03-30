using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

public class Department : BaseEntity.WithId<long>
{
    public string Name { get; set; }

    public long? ParentDepartmentId { get; set; }

    public Department ParentDepartment { get; set; }

    public ICollection<Department> ChildDepartments { get; set; }

    public DepartmentType DepartmentType { get; set; } = DepartmentType.Default;

    public string EnterpriseId { get; set; }

    public Enterprise Enterprise { get; set; }
}