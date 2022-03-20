using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using UserService.Contract.ViewModels;

namespace EnterpriseService.Contract.ViewModels;

public record EnterpriseCreateViewModel
(
    string Name,
    UserCreateViewModel UserCreate,
    DepartmentCreateViewModel DepartmentCreate
);