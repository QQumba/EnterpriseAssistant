namespace EnterpriseService.API.OneOfResponses;
    
public struct EnterpriseUserAlreadyExists
{
    public EnterpriseUserAlreadyExists(string enterpriseId, string userLogin)
    {
        EnterpriseId = enterpriseId;
        UserLogin = userLogin;
    }

    public string EnterpriseId { get; }

    public string UserLogin { get; }
}