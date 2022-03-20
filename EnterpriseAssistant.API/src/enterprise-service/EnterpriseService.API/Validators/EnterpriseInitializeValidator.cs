using EnterpriseService.Contract.ViewModels;
using FluentValidation;

namespace EnterpriseService.API.Validators;

public class EnterpriseInitializeValidator :  AbstractValidator<EnterpriseCreateViewModel>
{
    public EnterpriseInitializeValidator()
    {
        RuleFor(e => e.UserCreate).InjectValidator();
        RuleFor(e => e.DepartmentCreate).InjectValidator();
    }
}