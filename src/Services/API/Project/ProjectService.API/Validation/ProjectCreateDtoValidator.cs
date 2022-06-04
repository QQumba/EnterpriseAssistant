using FluentValidation;
using ProjectService.Contract.DataTransfer;

namespace ProjectService.API.Validation
{
    internal class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(60).MinimumLength(1).WithMessage("err");
        }
    }
}
