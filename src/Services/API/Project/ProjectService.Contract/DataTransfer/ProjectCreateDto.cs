namespace ProjectService.Contract.DataTransfer;

public class ProjectCreateDto
{
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = String.Empty;
};
