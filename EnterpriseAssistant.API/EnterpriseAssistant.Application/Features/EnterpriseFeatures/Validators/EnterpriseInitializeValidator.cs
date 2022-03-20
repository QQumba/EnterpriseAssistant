using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using FluentValidation;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.Validators;

public class EnterpriseInitializeValidator :  AbstractValidator<EnterpriseInitializeViewModel>
{
    public EnterpriseInitializeValidator()
    {
        RuleFor(e => e.UserCreate);
    }
}