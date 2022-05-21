using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations
{
    public class DepartmentUserConfiguration : IEntityTypeConfiguration<DepartmentUser>
    {
        public void Configure(EntityTypeBuilder<DepartmentUser> builder)
        {
            builder.ConfigureGeneratedId();

            builder.HasIndex(du => new { du.DepartmentId, du.UserId }).IsUnique();

            builder
                .HasOne<Enterprise>()
                .WithMany()
                .HasForeignKey(du => du.EnterpriseId);
        }
    }
}