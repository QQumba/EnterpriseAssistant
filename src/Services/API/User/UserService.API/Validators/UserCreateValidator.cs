using FluentValidation;
using UserService.Contract.DataTransfer;

namespace UserService.API.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
        RuleFor(u => u.Email).Must(e => e.Contains('@')).WithMessage("Email must contain @");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
    }
}