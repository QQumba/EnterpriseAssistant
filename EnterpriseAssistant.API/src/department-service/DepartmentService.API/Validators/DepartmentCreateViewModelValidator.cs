using DepartmentService.Contract.ViewModels;
using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using FluentValidation;

namespace DepartmentService.API.Validators;

public class DepartmentCreateViewModelValidator : AbstractValidator<DepartmentCreateViewModel>
{
    public DepartmentCreateViewModelValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage($"Department {nameof(DepartmentCreateViewModel.Name)} cannot be empty");
    }
}