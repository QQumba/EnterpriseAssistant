using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("managed_user")]
public class ManagedUser : BaseEntity
{
    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("is_email_confirmed")]
    public bool IsEmailConfirmed { get; set; }

    [Column("password")]
    public string Password { get; set; } = null!;

    // TODO: remove empty string initialization when add password hashing
    [Column("salt")]
    public string Salt { get; set; } = "";

    public ICollection<User>? Users { get; set; }
}