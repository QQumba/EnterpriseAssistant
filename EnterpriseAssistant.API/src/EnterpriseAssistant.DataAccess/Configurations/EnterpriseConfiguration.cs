﻿using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
{
    public void Configure(EntityTypeBuilder<Enterprise> builder)
    {
        builder.ToTable("enterprise");
        builder.ConfigureBaseEntity().ConfigureId<Enterprise, string>();

        builder.Property(e => e.DisplayedName).HasColumnName("name").IsRequired();
        builder.Ignore(e => e.RootDepartment);

        builder
            .HasMany<User>()
            .WithOne(u => u.Enterprise)
            .HasForeignKey(u => u.EnterpriseId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}