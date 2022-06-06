namespace EnterpriseService.Contract.DataTransfer;

public class InviteSubmitDto
{
    public string EnterpriseId { get; set; } = null!;

    public string Login { get; set; } = null!;
}