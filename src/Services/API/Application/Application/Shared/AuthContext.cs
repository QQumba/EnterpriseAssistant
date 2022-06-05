namespace EnterpriseAssistant.Application.Shared;

public class AuthContext
{
    public long UserId { get; init; }
    
    public string Email { get; init; } = null!;

    public string? Login { get; init; }
    
    public string? EnterpriseId { get; init; }
}