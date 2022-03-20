namespace EnterpriseAssistant.DataAccess.Entities;

public class Enterprise : BaseEntity.WithId<string>
{
    public string DisplayedName { get; set; }

    public ICollection<Department> Departments { get; set; }
    
    public Department RootDepartment => Departments.FirstOrDefault();
}