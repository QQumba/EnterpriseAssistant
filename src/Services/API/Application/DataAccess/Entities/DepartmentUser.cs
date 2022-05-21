using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("department_user")]
public class DepartmentUser : BaseEntity.WithId<long>
{
    [Column("department_id")]
    public long DepartmentId { get; set; }

    public Department Department { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    public User User { get; set; }

    [Column("department_user_role")]
    public DepartmentUserRole DepartmentUserRole { get; set; } = DepartmentUserRole.User;

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; }
}
