namespace Yape.TransactionService.Application
{
    public class TransactionCreatedEvent
    {
        public Guid TransactionId { get; set; }
        public Guid SourceAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
