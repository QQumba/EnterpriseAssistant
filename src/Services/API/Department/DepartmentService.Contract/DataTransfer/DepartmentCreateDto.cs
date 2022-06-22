namespace DepartmentService.Contract.DataTransfer;

public class DepartmentCreateDto
{
    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public long? ParentDepartmentId { get; set; }

    public IEnumerable<DepartmentAdminDto>? Admins { get; set; }

    public bool DoNotJoin { get; set; }

    public bool DisplayAsMember { get; set; }
}

public class DepartmentAdminDto
{
    public long Id { get; set; }

    public bool DisplayAsMember { get; set; }
}