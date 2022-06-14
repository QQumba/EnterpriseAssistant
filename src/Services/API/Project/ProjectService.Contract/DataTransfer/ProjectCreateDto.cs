using DepartmentService.Contract.DataTransfer;

namespace ProjectService.Contract.DataTransfer;

public class ProjectCreateDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public DepartmentCreateDto DepartmentCreate { get; set; } = null!;
};
