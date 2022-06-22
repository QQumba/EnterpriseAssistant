namespace EnterpriseService.Contract.DataTransfer;

public class EnterpriseUserDto
{
    public long Id { get; set; }
    
    public string? Login { get; set; }
    
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }
}