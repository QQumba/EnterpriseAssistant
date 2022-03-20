using EnterpriseService.Contranct.ViewModels;
using FluentValidation;

namespace EnterpriseService.API.Validators;

public class EnterpriseInitializeValidator :  AbstractValidator<EnterpriseInitializeViewModel>
{
    public EnterpriseInitializeValidator()
    {
        RuleFor(e => e.UserCreate).InjectValidator();
        RuleFor(e => e.EnterpriseCreate).InjectValidator();
        RuleFor(e => e.DepartmentCreate).InjectValidator();
    }
}