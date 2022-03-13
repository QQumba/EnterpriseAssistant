using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("department");
        builder.ConfigureBaseEntity().ConfigureGeneratedId();
        
        builder.Property(d => d.Name).HasColumnName("name").IsRequired();
        builder.Property(d => d.ParentDepartmentId).HasColumnName("parent_department_id").IsRequired(false);
        builder.Property(d => d.DepartmentType).HasColumnName("department_type").IsRequired();
        builder.Property(d => d.EnterpriseId).HasColumnName("enterprise_id").IsRequired();
        
        builder.HasMany<DepartmentUser>()
            .WithOne(du => du.Department)
            .HasForeignKey(du => du.DepartmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

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