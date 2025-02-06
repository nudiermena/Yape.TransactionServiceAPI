using Yape.TransactionService.Domain.Transactions;

namespace Yape.TransactionService.Application.Common.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<Guid> CreateFinancialTransactionAsync(Transaction transaction);
        public Task<Transaction> UpdateTransactionStatus(Guid id, string status);
    }
}