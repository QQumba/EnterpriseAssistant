namespace EnterpriseService.Contract.DataTransfer;

public class EnterpriseUserDto
{
    public long UserId { get; set; }
    
    public string Login { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }
}