namespace EnterpriseService.API.OneOfResponses;

public struct EnterpriseNotFoundError
{
    public EnterpriseNotFoundError(string enterpriseId)
    {
        EnterpriseId = enterpriseId;
    }

    public string EnterpriseId { get; }
}
