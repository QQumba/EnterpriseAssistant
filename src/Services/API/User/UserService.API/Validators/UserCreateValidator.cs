using FluentValidation;
using UserService.Contract.DataTransfer;

namespace UserService.API.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        // todo: add rules
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
    }
}