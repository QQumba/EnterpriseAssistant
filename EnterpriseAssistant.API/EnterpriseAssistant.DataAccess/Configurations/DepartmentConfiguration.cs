using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceBasedAuthenticationTest.Configurations;

namespace EnterpriseAssistant.DataAccess.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("department");
            builder.ConfigureBaseEntity();

            builder.Property(d => d.Name)
                .HasColumnName("name")
                .IsRequired();
            builder.Property(d => d.ParentDepartmentId)
                .HasColumnName("parent_department_id")
                .IsRequired(false);

            builder.HasMany<DepartmentUser>()
                .WithOne(du => du.Department)
                .HasForeignKey(du => du.DepartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.ChildDepartments)
                .WithOne(d => d.ParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}