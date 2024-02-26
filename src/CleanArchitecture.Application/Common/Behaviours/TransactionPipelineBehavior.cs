using CleanArchitecture.Application.Common.Interfaces.Persistence;
using MediatR;
using System.Transactions;

namespace CleanArchitecture.Application.Common.Behaviours;

public sealed class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Complete();
            await _unitOfWork.DisposeAsync();
            return response;
        }
    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}
