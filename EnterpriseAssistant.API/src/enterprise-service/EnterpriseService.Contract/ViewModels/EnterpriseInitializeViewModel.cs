using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using UserService.Contract.ViewModels;

namespace EnterpriseService.Contract.ViewModels;

public record EnterpriseInitializeViewModel
(
    UserCreateViewModel UserCreate,
    EnterpriseCreateViewModel EnterpriseCreate,
    DepartmentCreateViewModel DepartmentCreate
);