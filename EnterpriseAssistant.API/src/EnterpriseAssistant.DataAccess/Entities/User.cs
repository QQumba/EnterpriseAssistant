using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

/// <summary>
/// Enterprise user
/// </summary>
[Table("user")]
public class User : BaseEntity.WithId<long>
{
    [Column("login")]
    public string Login { get; set; } = null!;
    
    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("role")]
    public Role Role { get; set; } = Role.User;

    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("salt")]
    public string Salt { get; set; } = null!;

    public Enterprise? Enterprise { get; set; }

    public ManagedUser? ManagedUser { get; set; }

    [Column("managed_user_email")]
    public string? ManagedUserEmail { get; set; }
}