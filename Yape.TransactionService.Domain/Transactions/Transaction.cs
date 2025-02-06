using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yape.TransactionService.Domain.Transactions
{
    public class Transaction
    {
        public Transaction(Guid sourceAccountId, Guid targetAccountId, int transferTypeId, decimal value)
        {
            SourceAccountId = Guard.Against.NullOrEmpty(sourceAccountId);
            TargetAccountId = Guard.Against.NullOrEmpty(targetAccountId);
            TransferTypeId = transferTypeId;
            Value = value;
        }

        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("sourceaccountid")]
        public Guid SourceAccountId { get; set; }

        [Column("targetaccountid")]
        public Guid TargetAccountId { get; set; }

        [Column("transfertypeid")]
        public int TransferTypeId { get; set; }

        [Column("value")]
        public decimal Value { get; set; }

        [Column("status")]
        public string Status { get; set; } = YapeTransactionStatus.Pending.ToString();

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum YapeTransactionStatus
    {
        Pending,
        Approved,
        Rejected
    }
}