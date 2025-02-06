using MediatR;
using Yape.TransactionService.Application.Common.Interfaces;
using Yape.TransactionService.Domain.Transactions;
using MassTransit;
using Yape.TransactionService.Domain.Kafka;
using Yape.TransactionService.Contracts.Transactions.Commands;
using Ardalis.Result;

namespace Yape.TransactionService.Application.Transactions.Commands
{
    public class TransactionStatusUpdateCommandHandler : IRequestHandler<TransactionStatusUpdateCommand, Result<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionStatusUpdateCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;            
        }
        public async Task<Result<Transaction>> Handle(TransactionStatusUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _transactionRepository.UpdateTransactionStatus(request.TransactionId, request.Status);            
        }
    }
}
