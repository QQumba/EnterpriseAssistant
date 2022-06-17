using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace DepartmentService.Contract.DataTransfer;

public class DepartmentUserCreateDto
{
    public long UserId { get; set; }

    public DepartmentUserRole Role { get; set; } = DepartmentUserRole.User;

    public bool DisplayAsMember { get; set; } = true;
}