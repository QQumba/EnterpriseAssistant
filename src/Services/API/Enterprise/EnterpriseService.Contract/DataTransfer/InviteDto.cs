using EnterpriseAssistant.DataAccess.Entities;
using UserService.Contract.DataTransfer;

namespace EnterpriseService.Contract.DataTransfer;

public class InviteDto
{
    public InviteDto()
    {
    }

    public InviteDto(Invite invite)
    {
        EnterpriseId = invite.EnterpriseId;
    }

    public string EnterpriseId { get; set; } = null!;

    public string EnterpriseDisplayedName { get; set; } = null!;

    public UserDto? User { get; set; }
}