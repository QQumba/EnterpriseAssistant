namespace EnterpriseAssistant.Application.Errors;

public class BadRequest : IBadRequestError
{
    public BadRequest(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}