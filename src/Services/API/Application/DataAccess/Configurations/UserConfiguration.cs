using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ConfigureGeneratedId();

            builder
                .HasMany<DepartmentUser>()
                .WithOne(du => du.User)
                .HasForeignKey(du => du.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(u => u.Enterprise)
                .WithMany(e => e.Users)
                .HasForeignKey(u => u.EnterpriseId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(u => new { u.Login,u.EnterpriseId }).IsUnique();
        }
    }
}