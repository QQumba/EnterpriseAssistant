using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("task_user")]
public class TaskUser : BaseEntity.WithId<long>
{
    [Column("user_id")]
    public long UserId { get; set; }

    public User? User { get; set; }

    [Column("task_id")]
    public long TaskId { get; set; }

    public Task? Task { get; set; }

    [Column("hours_spent")]
    public double? HoursSpent { get; set; }

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;
}

public class TaskUserConfiguration : IEntityTypeConfiguration<TaskUser>
{
    public void Configure(EntityTypeBuilder<TaskUser> builder)
    {
        builder.ConfigureGeneratedId();

        builder.HasIndex(tu => new { tu.UserId, tu.TaskId }).IsUnique();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Task)
            .WithMany()
            .HasForeignKey(x => x.TaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}