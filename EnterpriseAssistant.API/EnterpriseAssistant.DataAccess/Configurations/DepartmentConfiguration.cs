using EnterpriseAssistant.DataAccess.Configurations;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceBasedAuthenticationTest.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("department");
            builder.ConfigureBaseEntity().ConfigureId();

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

            builder.HasMany<Department>()
                .WithOne(d => d.ParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}