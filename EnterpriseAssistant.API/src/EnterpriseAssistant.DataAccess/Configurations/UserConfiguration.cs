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
            builder.ConfigureId<User,long>();
            
            builder.Property(u => u.FirstName).HasColumnName("first_name").IsRequired();
            builder.Property(u => u.LastName).HasColumnName("last_name").IsRequired(false);
            builder.Property(u => u.Role).HasColumnName("role").IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").IsRequired();
            builder.Property(u => u.Salt).HasColumnName("salt").IsRequired();

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