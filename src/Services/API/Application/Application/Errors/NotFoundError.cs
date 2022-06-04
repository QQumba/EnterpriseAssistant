namespace EnterpriseAssistant.Application.Errors;

public class NotFoundError : INotFoundError
{
    public NotFoundError(string message)
    {
        Message = message;
    }

    public string Message { get; }
}