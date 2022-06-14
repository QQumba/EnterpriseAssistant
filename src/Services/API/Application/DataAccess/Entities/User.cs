using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

/// <summary>
/// Enterprise user
/// </summary>
[Table("user")]
public class User : BaseEntity.WithId<long>
{
    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("salt")]
    public string Salt { get; set; } = null!;

    public IEnumerable<EnterpriseUser>? EnterpriseUsers { get; set; }

    public IEnumerable<DepartmentUser>? DepartmentUsers { get; set; }
}