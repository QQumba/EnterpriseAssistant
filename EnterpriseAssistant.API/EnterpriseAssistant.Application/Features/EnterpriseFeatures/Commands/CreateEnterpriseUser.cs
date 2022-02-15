using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;
using EnterpriseAssistant.Application.Shared;
using MediatR;
using OneOf;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.Commands;

public class CreateEnterpriseUser : IRequest<OneOf<UserViewModel, Error.NotFound>>
{
    
}