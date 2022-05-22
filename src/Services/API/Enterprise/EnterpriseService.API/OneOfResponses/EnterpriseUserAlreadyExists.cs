using EnterpriseAssistant.Application.Errors;

namespace EnterpriseService.API.OneOfResponses;

public readonly struct EnterpriseUserAlreadyExists : IBadRequestError
{
    private const string MessageTemplate = "User with email {0} already exists in enterprise {1}";
    
    public EnterpriseUserAlreadyExists(string enterpriseId, string userLogin)
    {
        EnterpriseId = enterpriseId;
        UserLogin = userLogin;
    }

    public string EnterpriseId { get; }

    public string UserLogin { get; }

    public string Message => string.Format(MessageTemplate, UserLogin, EnterpriseId);
}