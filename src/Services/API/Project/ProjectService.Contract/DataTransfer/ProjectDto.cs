namespace ProjectService.Contract.DataTransfer;

public class ProjectDto
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public long DepartmentId { get; set; }

    public string DepartmentCode { get; set; } = null!;
}