namespace ProjectService.Contract.DataTransfer;

public class ProjectCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public string EnterpriseId { get; set; } = null!;
    public long DepartmentId { get; set; } 
};
