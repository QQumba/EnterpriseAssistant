﻿using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess.Entities;
using EnterpriseAssistant.DataAccess.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Task = EnterpriseAssistant.DataAccess.Entities.Tasks.Task;

namespace EnterpriseAssistant.DataAccess;

public class EnterpriseAssistantDbContext : DbContext
{
    public EnterpriseAssistantDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    // public DbSet<Enterprise> Enterprises { get; set; }

    // public DbSet<Department> Departments { get; set; }

    // public DbSet<DepartmentUser> DepartmentUsers { get; set; }

    // public DbSet<Project> Projects { get; set; }

    // public DbSet<EnterpriseUser> EnterpriseUsers { get; set; }

    // public DbSet<Invite> Invites { get; set; }

    public DbSet<Task> Tasks { get; set; }
    
    public DbSet<Tag> Tags { get; set; }

    public DbSet<TaskUser> TaskUsers { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity &&
                        e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            entity.UpdatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                if (entity is BaseEntity.WithId<Guid> guidEntity)
                {
                    guidEntity.Id = Guid.NewGuid();
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnterpriseAssistantDbContext).Assembly);
    }
}