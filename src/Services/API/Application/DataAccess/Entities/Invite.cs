using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Configurations;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Entities;

[Table("invite")]
public class Invite : BaseEntity.WithId<long>
{
    [Column("user_id")]
    public long? UserId { get; set; }

    [Column("user_email")]
    public string UserEmail { get; set; } = null!;

    public User? User { get; set; }

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;

    public Enterprise? Enterprise { get; set; }

    [Column("invite_status")]
    public InviteStatus Status { get; set; } = InviteStatus.Pending;

    public EnterpriseUser? Accept(string login)
    {
        if (UserId.HasValue == false)
        {
            return null;
        }
        Status = InviteStatus.Accepted;
        return new EnterpriseUser
        {
            UserId = UserId.Value,
            EnterpriseId = EnterpriseId,
            Login = login,
            Role = EnterpriseUserRole.User,
        };
    }
}

public enum InviteStatus
{
    Pending = 0,
    Accepted
}

public class InviteConfiguration : IEntityTypeConfiguration<Invite>
{
    public void Configure(EntityTypeBuilder<Invite> builder)
    {
        builder.ConfigureGeneratedId();
        
        builder.HasIndex(i => new { i.UserId, i.EnterpriseId }).IsUnique();

        builder.HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(i => i.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(i => i.Enterprise)
            .WithMany()
            .HasForeignKey(i => i.EnterpriseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}