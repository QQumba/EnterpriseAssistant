using EnterpriseAssistant.DataAccess.Entities;

namespace EnterpriseService.Contract.DataTransfer;

public class InviteDto
{
    public InviteDto()
    {
    }

    public InviteDto(Invite invite)
    {
        EnterpriseId = invite.EnterpriseId;
        Status = invite.Status;
        UserEmail = invite.UserEmail;
    }
    
    public string EnterpriseId { get; set; } = null!;

    public string EnterpriseDisplayedName { get; set; }

    public string UserEmail { get; set; }
    
    public InviteStatus Status { get; set; }
}