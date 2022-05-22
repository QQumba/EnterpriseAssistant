namespace UserService.Contract.DataTransfer;

public class ManagedUserDto
{
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
}