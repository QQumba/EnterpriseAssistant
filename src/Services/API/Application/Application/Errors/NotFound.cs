namespace EnterpriseAssistant.Application.Errors;

public class NotFound : INotFoundError
{
    public NotFound(string message)
    {
        Message = message;
    }

    public string Message { get; }
}