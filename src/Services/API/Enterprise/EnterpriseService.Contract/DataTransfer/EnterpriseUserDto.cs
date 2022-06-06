namespace EnterpriseService.Contract.DataTransfer;

public class EnterpriseUserDto
{
    public string Login { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }
}