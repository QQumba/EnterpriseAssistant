using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseAssistant.DataAccess.Configurations;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseAssistant.DataAccess.Entities;

public class Invite : BaseEntity.WithId<long>
{
    [Column("user_id")]
    public long UserId { get; set; }

    public User? User { get; set; }

    [Column("enterprise_id")]
    public string EnterpriseId { get; set; } = null!;

    public Enterprise? Enterprise { get; set; }
    
    [Column("invite_status")]
    public InviteStatus Status { get; set; }

    public EnterpriseUser Accept(string login)
    {
        Status = InviteStatus.Accepted;
        return new EnterpriseUser
        {
            UserId = UserId,
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
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(i => i.Enterprise)
            .WithMany()
            .HasForeignKey(i => i.EnterpriseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}