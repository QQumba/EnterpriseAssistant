using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.ConfigureBaseEntity();

            builder.Property(u => u.Name).HasColumnName("name").IsRequired();
            builder.Property(u => u.Login).HasColumnName("login").IsRequired();

            builder
                .HasMany<DepartmentUser>()
                .WithOne(du => du.User)
                .HasForeignKey(du => du.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(u => u.Login).IsUnique();
        }
    }
}