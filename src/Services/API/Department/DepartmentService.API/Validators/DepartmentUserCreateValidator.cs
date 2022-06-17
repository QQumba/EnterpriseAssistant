using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using FluentValidation;

namespace DepartmentService.API.Validators;

public class DepartmentUserCreateValidator : AbstractValidator<DepartmentUserCreateDto>
{
    public DepartmentUserCreateValidator()
    {
        When(du => du.Role != DepartmentUserRole.Admin, () =>
        {
            RuleFor(du => du.DisplayAsMember)
                .Must(d => d)
                .WithMessage($"Display as member can be false only for users with role {nameof(DepartmentUserRole.Admin)}");
        });
    }
}