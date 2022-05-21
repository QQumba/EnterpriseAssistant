using FluentValidation;
using UserService.Contract.ViewModels;

namespace UserService.API.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateViewModel>
{
    public UserCreateValidator()
    {
        // todo: add rules
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
    }
}