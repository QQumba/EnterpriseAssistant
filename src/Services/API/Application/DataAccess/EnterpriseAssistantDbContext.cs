using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAssistant.DataAccess;

public class EnterpriseAssistantDbContext : DbContext
{
    public EnterpriseAssistantDbContext(DbContextOptions<EnterpriseAssistantDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    public DbSet<Enterprise> Enterprises { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<DepartmentUser> DepartmentUsers { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<EnterpriseUser> EnterpriseUsers { get; set; }

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
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var now = DateTime.UtcNow;
        
        var user = new User
        {
            CreatedAt = now,
            UpdatedAt = now,
            Id = 1,
            Email = "test@mail.com",
            FirstName = "Test",
            LastName = "User",
            Password = "qwe",
            Salt = "test_salt"
        };
        
        var enterprise = new Enterprise
        {
            CreatedAt = now,
            UpdatedAt = now,
            Id = "test",
            DisplayedName = "test"
        };

        var enterpriseUser = new EnterpriseUser
        {
            CreatedAt = now,
            UpdatedAt = now,
            Id = 1,
            UserId = 1,
            EnterpriseId = "test",
            Login = "test"
        };

        var department = new Department
        {
            CreatedAt = now,
            UpdatedAt = now,
            Id = 1,
            Name = "Test department",
            EnterpriseId = "test"
        };
        
        var departmentUser = new DepartmentUser
        {
            CreatedAt = now,
            UpdatedAt = now,
            Id = 1,
            UserId = 1,
            DepartmentId = 1,
            EnterpriseId = "test"
        };

        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<Enterprise>().HasData(enterprise);
        modelBuilder.Entity<EnterpriseUser>().HasData(enterpriseUser);
        modelBuilder.Entity<Department>().HasData(department);
        modelBuilder.Entity<DepartmentUser>().HasData(departmentUser);
    }
}