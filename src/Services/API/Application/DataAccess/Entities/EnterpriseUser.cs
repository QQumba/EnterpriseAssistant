using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("enterprise_user")]
public class EnterpriseUser : BaseEntity.WithId<long>
{
    [Column("user_id")]
    public long UserId { get; set; }
    
    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;

    [Column("login")]
    public string Login { get; set; } = null!;

    [Column("role")]
    public EnterpriseUserRole Role { get; set; }

    public User User { get; set; } = null!;

    public Enterprise Enterprise { get; set; } = null!;
}