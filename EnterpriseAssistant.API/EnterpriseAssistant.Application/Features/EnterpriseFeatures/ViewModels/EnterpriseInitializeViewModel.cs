using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;

public record EnterpriseInitializeViewModel
(
    UserCreateViewModel UserCreate,
    DepartmentCreateViewModel DepartmentCreate
);