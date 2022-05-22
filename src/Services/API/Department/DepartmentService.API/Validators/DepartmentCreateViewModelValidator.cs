using DepartmentService.Contract.DataTransfer;
using FluentValidation;

namespace DepartmentService.API.Validators;

public class DepartmentCreateViewModelValidator : AbstractValidator<DepartmentCreateDto>
{
    public DepartmentCreateViewModelValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage($"Department {nameof(DepartmentCreateDto.Name)} cannot be empty");
    }
}