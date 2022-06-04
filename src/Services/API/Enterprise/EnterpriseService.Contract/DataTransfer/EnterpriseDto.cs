namespace EnterpriseService.Contract.DataTransfer;

public record EnterpriseDto
{
    public string Id { get; init; }

    public string DisplayedName { get; init; }
}