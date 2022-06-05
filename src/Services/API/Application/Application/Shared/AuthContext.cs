namespace EnterpriseAssistant.Application.Shared;

public class AuthContext
{
    public long UserId { get; init; }
    
    public string Email { get; init; } = null!;

    public string? EnterpriseId { get; init; }
}