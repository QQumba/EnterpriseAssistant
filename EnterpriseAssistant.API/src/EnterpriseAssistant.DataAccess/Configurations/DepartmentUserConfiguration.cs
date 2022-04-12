using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations
{
    public class DepartmentUserConfiguration : IEntityTypeConfiguration<DepartmentUser>
    {
        public void Configure(EntityTypeBuilder<DepartmentUser> builder)
        {
            builder.ToTable("department_user");
            builder.ConfigureBaseEntity().ConfigureGeneratedId();

            builder.Property(du => du.DepartmentId).HasColumnName("department_id").IsRequired();
            builder.Property(du => du.DepartmentUserType).HasColumnName("department_user_type").IsRequired();
            builder.Property(du => du.EnterpriseId).HasColumnName("enterprise_id").IsRequired();
            
            builder.HasIndex(du => new { du.DepartmentId, du.UserId }).IsUnique();

            builder
                .HasOne<Enterprise>()
                .WithMany()
                .HasForeignKey(du => du.EnterpriseId);
        }
    }
}