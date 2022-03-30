namespace EnterpriseAssistant.DataAccess.Entities;

public class ManagedUser
{
    public string Email { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public User User { get; set; }
    
    public string UserLogin { get; set; }
}