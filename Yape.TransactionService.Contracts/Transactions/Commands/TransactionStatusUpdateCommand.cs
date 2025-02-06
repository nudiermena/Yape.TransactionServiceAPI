using Ardalis.Result;
using MediatR;
using System;
using Yape.TransactionService.Domain.Transactions;

namespace Yape.TransactionService.Contracts.Transactions.Commands
{
    public record TransactionStatusUpdateCommand(Guid TransactionId, string Status) : IRequest<Result<Transaction>>;

}
