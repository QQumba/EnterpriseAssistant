using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

public class DepartmentUser : BaseEntity.WithId<long>
{
    public long DepartmentId { get; set; }

    public Department Department { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    public User User { get; set; }

    public DepartmentUserType DepartmentUserType { get; set; } = DepartmentUserType.User;

    public string EnterpriseId { get; set; }
}
