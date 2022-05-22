using EnterpriseAssistant.Application.Errors;

namespace EnterpriseService.API.OneOfResponses;

public readonly struct EnterpriseNotFoundError : INotFoundError
{
    private const string MessageTemplate = "Enterprise with id '{0}' not found";

    public EnterpriseNotFoundError(string enterpriseId)
    {
        EnterpriseId = enterpriseId;
    }

    public string EnterpriseId { get; }

    public string Message => string.Format(MessageTemplate, EnterpriseId);
}

public readonly struct DepartmentNotFoundError : INotFoundError
{
    private const string MessageTemplate = "Department with id '{0}' not found";

    public DepartmentNotFoundError(string departmentId)
    {
        DepartmentId = departmentId;
    }

    public string DepartmentId { get; }

    public string Message => string.Format(MessageTemplate, DepartmentId);
}