using FluentValidation;

namespace EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;

public class DepartmentCreateViewModelValidator : AbstractValidator<DepartmentCreateViewModel>
{
    public DepartmentCreateViewModelValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage($"Department {nameof(DepartmentCreateViewModel.Name)} cannot be empty");
    }
}