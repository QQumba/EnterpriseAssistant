namespace EnterpriseAssistant.Application.Errors;

public class BadRequestError : IBadRequestError
{
    public BadRequestError(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}