using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("enterprise")]
public class Enterprise : BaseEntity.WithId<string>
{
    [Column("displayed_name")]
    public string DisplayedName { get; set; } = null!;

    public ICollection<Department>? Departments { get; set; }

    public ICollection<User>? Users { get; set; }

    [Column("owner_email")]
    public string OwnerEmail { get; set; } = null!;

    public ManagedUser? Owner { get; set; }

    [NotMapped]
    public Department? RootDepartment => Departments?.FirstOrDefault(d => d.DepartmentType == DepartmentType.Root);
}