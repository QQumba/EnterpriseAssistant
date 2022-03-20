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
            builder.ConfigureBaseEntity().ConfigureKey(u => u.Login, "login");

            builder.Property(u => u.FirstName).HasColumnName("first_name").IsRequired();
            builder.Property(u => u.LastName).HasColumnName("last_name").IsRequired(false);
            builder.Property(u => u.Role).HasColumnName("role").IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").IsRequired();
            builder.Property(u => u.Salt).HasColumnName("salt").IsRequired();

            builder
                .HasMany<DepartmentUser>()
                .WithOne(du => du.User)
                .HasForeignKey(du => du.UserLogin)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}