namespace DepartmentService.Contract.DataTransfer;

public class DepartmentCreateDto
{
    public string Name { get; set; } = null!;

    public long? ParentDepartmentId { get; set; }

    public IEnumerable<long>? AdminIds { get; set; }
    
    public bool DoNotJoin { get; set; }

    public bool JoinAsMember { get; set; }
}