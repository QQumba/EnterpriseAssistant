using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

/// <summary>
/// Enterprise user
/// </summary>
public class User : BaseEntity.WithId<long>
{
    [Column("login")]
    public string Login { get; set; } = null!;
    
    public string EnterpriseId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public Role Role { get; set; } = Role.User;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public Enterprise? Enterprise { get; set; }

    public ManagedUser? ManagedUser { get; set; }

    [Column("managed_user_email")]
    public string? ManagedUserEmail { get; set; }
}