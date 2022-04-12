using System.Linq.Expressions;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Configurations;

public static class BaseEntityEntityTypeBuilderExtensions
{
    public static void ConfigureId<TEntity, TId>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity.WithId<TId>
    {
        builder.ConfigureKey(e => e.Id);
    }

    public static void ConfigureKey<T>(this EntityTypeBuilder<T> builder,
        Expression<Func<T, object?>> property, string name = "id") where T : BaseEntity
    {
        builder.HasKey(property);
        builder.HasIndex(property).IsUnique();
        builder.Property(property).HasColumnName(name).IsRequired();
    }

    public static void ConfigureGeneratedId<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity.WithId<long>
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
    }
}