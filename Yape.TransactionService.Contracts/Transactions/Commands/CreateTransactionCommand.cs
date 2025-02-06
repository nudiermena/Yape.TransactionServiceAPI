using Ardalis.Result;
using MediatR;

namespace Yape.TransactionService.Contracts.Transactions.Commands
{
    public record CreateTransactionCommand(Guid SourceAccountId, Guid TargetAccountId, int TransferTypeId, decimal Value) : IRequest<Result<Guid>>;
}
