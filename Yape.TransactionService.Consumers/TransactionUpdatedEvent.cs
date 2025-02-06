using System;

namespace Yape.TransactionService.Consumers
{
    public class TransactionUpdatedEvent
    {
        public Guid TransactionId { get; set; }                        
        public string Status { get; set; } = string.Empty;
    }
}
