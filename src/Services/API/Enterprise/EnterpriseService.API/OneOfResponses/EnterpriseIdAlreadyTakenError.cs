namespace EnterpriseService.API.OneOfResponses;

public struct EnterpriseIdAlreadyTakenError
{
    public EnterpriseIdAlreadyTakenError(string takenId)
    {
        TakenId = takenId;
    }

    public string TakenId { get; }
}