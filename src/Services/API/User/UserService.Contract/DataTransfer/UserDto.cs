namespace UserService.Contract.DataTransfer;

public class UserDto
{
    public long Id { get; set; }
    
    public string Email { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }
}