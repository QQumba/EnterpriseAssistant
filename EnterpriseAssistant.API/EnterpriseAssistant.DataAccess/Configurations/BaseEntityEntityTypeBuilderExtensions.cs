using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceBasedAuthenticationTest.Models;

namespace ResourceBasedAuthenticationTest.Configurations
{
    public static class BaseEntityEntityTypeBuilderExtensions
    {
        public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            builder.Property(e => e.IsSoftDeleted).HasColumnName("is_soft_deleted").IsRequired();
        }
    }
}