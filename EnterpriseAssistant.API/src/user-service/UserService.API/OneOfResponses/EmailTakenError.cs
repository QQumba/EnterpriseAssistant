namespace UserService.API.OneOfResponses;

public struct EmailTakenError
{
    public EmailTakenError(string email)
    {
        Email = email;
    }

    public string Email { get; }
}