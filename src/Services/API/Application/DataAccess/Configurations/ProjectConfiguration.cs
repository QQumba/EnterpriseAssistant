using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ConfigureGeneratedId();
            builder.HasIndex(u => new { u.EnterpriseId, u.Name}).IsUnique();

            builder.HasOne(eu => eu.Enterprise)
                .WithMany()
                .HasForeignKey(e => e.EnterpriseId)
                .IsRequired();

            builder.HasOne(p => p.Department)
                 .WithOne(e => e.Project)
                 .HasForeignKey<Department>(a => a.ProjectId)
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
