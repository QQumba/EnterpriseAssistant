using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using MediatR;
using OneOf;

namespace DepartmentService.API.Controllers;

public class GetUserDepartmentsCommand : IRequest<OneOf<IEnumerable<DepartmentDto>>>
{
}