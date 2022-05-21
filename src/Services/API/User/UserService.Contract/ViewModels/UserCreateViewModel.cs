namespace UserService.Contract.ViewModels;

public class UserCreateViewModel
{
    public string Login { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;
}