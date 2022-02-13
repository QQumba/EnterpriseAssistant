namespace EnterpriseAssistant.DataAccess.Entities;

public class Enterprise : BaseEntity.WithId<Guid>
{
    public string Name { get; set; }

    public ICollection<Department> Departments { get; set; }
}