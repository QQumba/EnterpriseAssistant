using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public class EnterpriseUserConfiguration : IEntityTypeConfiguration<EnterpriseUser>
{
    public void Configure(EntityTypeBuilder<EnterpriseUser> builder)
    {
        builder.ConfigureGeneratedId();

        builder.HasIndex(e => new { e.UserId, e.Login, e.EnterpriseId })
            .IsUnique();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.HasOne<Enterprise>()
            .WithMany()
            .HasForeignKey(e => e.EnterpriseId)
            .IsRequired();
    }
}