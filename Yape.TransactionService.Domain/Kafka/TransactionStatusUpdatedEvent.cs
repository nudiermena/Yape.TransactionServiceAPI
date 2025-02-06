using Yape.TransactionService.Domain.Transactions;

namespace Yape.TransactionService.Domain.Kafka
{
    public interface TransactionStatusUpdatedEvent
    {
        Guid TransactionId { get; }
        YapeTransactionStatus Status { get; }
    }
}