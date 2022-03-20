using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Role Role { get; set; } = Role.User;

    public string Password { get; set; }

    public string Salt { get; set; }
}