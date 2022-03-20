using EnterpriseService.Contranct.ViewModels;
using FluentValidation;

namespace EnterpriseService.API.Validators;

public class EnterpriseInitializeValidator :  AbstractValidator<EnterpriseInitializeViewModel>
{
    public EnterpriseInitializeValidator()
    {
        RuleFor(e => e.UserCreate);
    }
}