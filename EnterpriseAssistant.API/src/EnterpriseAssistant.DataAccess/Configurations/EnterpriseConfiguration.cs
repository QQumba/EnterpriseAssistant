using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
{
    public void Configure(EntityTypeBuilder<Enterprise> builder)
    {
        builder.ConfigureId<Enterprise, string>();
    }
}