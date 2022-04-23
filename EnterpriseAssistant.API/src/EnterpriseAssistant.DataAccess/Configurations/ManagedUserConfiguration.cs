using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public class ManagedUserConfiguration : IEntityTypeConfiguration<ManagedUser>
{
    public void Configure(EntityTypeBuilder<ManagedUser> builder)
    {
        builder.ConfigureKey(mu => mu.Email, "email");

        builder
            .HasMany(mu => mu.Users)
            .WithOne(u => u.ManagedUser)
            .HasForeignKey(u => u.ManagedUserEmail)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(mu => mu.Enterprises)
            .WithOne(e => e.Owner)
            .HasForeignKey(u => u.OwnerEmail)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}