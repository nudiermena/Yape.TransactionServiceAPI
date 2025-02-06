using Ardalis.Result;
using MediatR;

namespace Yape.TransactionService.Application.Transactions.Queries.GetTransaction
{
    public record TransactionDetailsQuery(Guid transactionId) : IRequest<Result<TransactionDetailsResponse>>;      
}
