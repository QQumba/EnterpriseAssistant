using FluentValidation;
using UserService.Contract.DataTransfer;

namespace UserService.API.Validators;

public class ManagedUserCreateValidator : AbstractValidator<ManagedUserCreateDto>
{
    public ManagedUserCreateValidator()
    {
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
        RuleFor(u => u.Email).Must(e => e.Contains('@')).WithMessage("Email must contain @");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
    }
}