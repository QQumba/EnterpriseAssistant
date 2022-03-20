using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using UserService.Contract.ViewModels;

namespace EnterpriseService.Contranct.ViewModels;

public record EnterpriseInitializeViewModel
(
    UserCreateViewModel UserCreate,
    EnterpriseCreateViewModel EnterpriseCreate,
    DepartmentCreateViewModel DepartmentCreate
);