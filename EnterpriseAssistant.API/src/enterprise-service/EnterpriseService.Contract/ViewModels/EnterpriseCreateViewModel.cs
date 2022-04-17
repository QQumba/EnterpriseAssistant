using DepartmentService.Contract.ViewModels;
using UserService.Contract.ViewModels;

namespace EnterpriseService.Contract.ViewModels;

public record EnterpriseCreateViewModel
(
    string Id,
    string DisplayedName,
    DepartmentCreateViewModel DepartmentCreate,
    UserCreateViewModel UserCreate
);