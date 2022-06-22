namespace DepartmentService.Contract.DataTransfer;

public class DepartmentDto
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public long? ParentDepartmentId { get; set; }

    public IList<DepartmentDto> ChildDepartments { get; set; } = new List<DepartmentDto>();
}