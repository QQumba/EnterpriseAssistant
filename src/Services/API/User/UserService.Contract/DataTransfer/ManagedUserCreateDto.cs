namespace UserService.Contract.DataTransfer;

public class ManagedUserCreateDto
{
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
}