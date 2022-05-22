namespace EnterpriseAssistant.Application.Errors;

public interface IErrorResponse
{
    public string Message { get; }
}

public interface INotFoundError : IErrorResponse
{
}

public interface IBadRequestError : IErrorResponse
{
}