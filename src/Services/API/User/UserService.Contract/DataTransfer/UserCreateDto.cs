namespace UserService.Contract.DataTransfer;

public class UserCreateDto
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;
}