namespace DepartmentService.Contract.DataTransfer;

public class DepartmentDto
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? ParentDepartmentId { get; set; }

    public IList<DepartmentDto> ChildDepartments { get; set; } = new List<DepartmentDto>();
}