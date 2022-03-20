using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using UserService.Contract.ViewModels;

namespace EnterpriseService.Contract.ViewModels;

public record EnterpriseCreateViewModel
(
    string Id,
    string DisplayedName,
    UserCreateViewModel UserCreate,
    DepartmentCreateViewModel DepartmentCreate
);