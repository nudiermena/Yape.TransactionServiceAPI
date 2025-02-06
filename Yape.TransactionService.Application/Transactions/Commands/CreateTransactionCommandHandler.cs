using MediatR;
using Yape.TransactionService.Application.Common.Interfaces;
using Yape.TransactionService.Domain.Transactions;
using MassTransit;
using Ardalis.Result;
using Yape.TransactionService.Contracts.Transactions.Commands;

namespace Yape.TransactionService.Application.Transactions.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITopicProducer<TransactionCreatedEvent> _topicProducer;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, ITopicProducer<TransactionCreatedEvent> topicProducer)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository)); ;
            _topicProducer = topicProducer ?? throw new ArgumentNullException(nameof(topicProducer));
        }
        public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction(request.SourceAccountId, request.TargetAccountId, request.TransferTypeId, request.Value);

            var transactionId = await _transactionRepository.CreateFinancialTransactionAsync(transaction);

            await _topicProducer.Produce(new TransactionCreatedEvent
            {
                TransactionId = transactionId,
                SourceAccountId = request.SourceAccountId,
                Amount = request.Value,
                CreatedAt = DateTime.UtcNow,
                Status = YapeTransactionStatus.Pending.ToString()
            }, cancellationToken);

            return transaction.Id;
        }
    }
}
