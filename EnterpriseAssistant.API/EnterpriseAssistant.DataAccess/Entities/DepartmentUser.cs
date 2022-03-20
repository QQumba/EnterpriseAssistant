using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

public class DepartmentUser : BaseEntity.WithId<long>
{
    public long DepartmentId { get; set; }

    public Department Department { get; set; }

    public string UserLogin { get; set; }

    public User User { get; set; }

    public DepartmentUserType DepartmentUserType { get; set; } = DepartmentUserType.User;
}