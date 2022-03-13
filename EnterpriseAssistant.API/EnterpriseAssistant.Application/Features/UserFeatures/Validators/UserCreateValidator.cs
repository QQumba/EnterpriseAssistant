using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using FluentValidation;

namespace EnterpriseAssistant.Application.Features.UserFeatures.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateViewModel>
{
    public UserCreateValidator()
    {
        // todo: add rules
    }
}