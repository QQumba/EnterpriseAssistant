using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using MediatR;
using OneOf;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.Commands;

public class InitiateEnterpriseCreateTransaction : IRequest<OneOf<EnterpriseCreateTransaction>>
{
}

public class InitiateEnterpriseCreateTransactionHandler : IRequestHandler<InitiateEnterpriseCreateTransaction,
    OneOf<EnterpriseCreateTransaction>>
{
    public Task<OneOf<EnterpriseCreateTransaction>> Handle(InitiateEnterpriseCreateTransaction request,
        CancellationToken cancellationToken)
    {
        var transaction = new EnterpriseCreateTransaction
        {
            Id = Guid.NewGuid()
        };

        return Task.FromResult(OneOf<EnterpriseCreateTransaction>.FromT0(transaction));
    }
}