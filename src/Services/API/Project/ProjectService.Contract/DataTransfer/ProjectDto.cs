using EnterpriseService.Contract.DataTransfer;

namespace ProjectService.Contract.DataTransfer;

public class ProjectDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long? Enterpriseid { get; set; }

}
