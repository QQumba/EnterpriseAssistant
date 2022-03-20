using EnterpriseService.Contract.ViewModels;
using FluentValidation;

namespace EnterpriseService.API.Validators;

public class EnterpriseCreateValidator :  AbstractValidator<EnterpriseCreateViewModel>
{
    public EnterpriseCreateValidator()
    {
        
        RuleFor(e => e.UserCreate).InjectValidator();
        RuleFor(e => e.DepartmentCreate).InjectValidator();
    }
}