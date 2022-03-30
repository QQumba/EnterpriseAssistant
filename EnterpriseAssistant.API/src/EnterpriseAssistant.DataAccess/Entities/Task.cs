namespace EnterpriseAssistant.DataAccess.Entities;

// todo: add configuration
public class Task : BaseEntity.WithId<long>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string UserLogin { get; set; }

    public User User { get; set; }

    public string EnterpriseId { get; set; }
}