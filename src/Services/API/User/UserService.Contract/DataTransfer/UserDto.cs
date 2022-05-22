namespace UserService.Contract.DataTransfer;

public class UserDto
{
    public string Login { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }
}