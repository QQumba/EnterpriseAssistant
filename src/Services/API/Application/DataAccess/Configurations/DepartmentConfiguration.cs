using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ConfigureGeneratedId();

        builder.HasIndex(d => new {d.Name, d.EnterpriseId, d.IsSoftDeleted}).IsUnique();
        
        builder.HasMany(d => d.ChildDepartments)
            .WithOne(d => d.ParentDepartment)
            .HasForeignKey(d => d.ParentDepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(d => d.Enterprise)
            .WithMany(e => e.Departments)
            .HasForeignKey(d => d.EnterpriseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}