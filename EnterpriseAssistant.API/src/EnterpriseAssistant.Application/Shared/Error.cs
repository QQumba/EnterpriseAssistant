using Microsoft.AspNetCore.Connections.Features;

namespace EnterpriseAssistant.Application.Shared;

public static class Error
{
    public struct NotFound : IError
    {
        public NotFound(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}

public interface IError
{
    public string Message { get; }
}