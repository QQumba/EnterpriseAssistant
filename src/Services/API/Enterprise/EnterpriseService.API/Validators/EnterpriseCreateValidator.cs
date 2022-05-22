using EnterpriseService.Contract.DataTransfer;
using FluentValidation;

namespace EnterpriseService.API.Validators;

public class EnterpriseCreateValidator : AbstractValidator<EnterpriseCreateDto>
{
    public EnterpriseCreateValidator()
    {
        RuleFor(e => e.Id).MaximumLength(50)
            .WithMessage(e => $"Max enterprise id length is 50, provided id length: {e.Id.Length}");
        RuleFor(e => e.UserCreate).InjectValidator();
        RuleFor(e => e.DepartmentCreate).InjectValidator();
    }
}