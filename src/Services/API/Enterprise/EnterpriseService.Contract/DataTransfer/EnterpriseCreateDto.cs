using DepartmentService.Contract.DataTransfer;
using UserService.Contract.DataTransfer;

namespace EnterpriseService.Contract.DataTransfer;

public record EnterpriseCreateDto
(
    string Id,
    string DisplayedName,
    DepartmentCreateDto DepartmentCreate,
    UserCreateDto UserCreate
);