using DepartmentService.Contract.DataTransfer;
using UserService.Contract.DataTransfer;

namespace EnterpriseService.Contract.DataTransfer;

public record EnterpriseCreateDto
{
    public string Id { get; set; }

    public string DisplayedName { get; set; }

    public string UserLogin { get; set; }

    public DepartmentCreateDto DepartmentCreate { get; set; }
}