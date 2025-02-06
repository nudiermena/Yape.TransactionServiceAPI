using Microsoft.EntityFrameworkCore;
using Yape.TransactionService.Application.Common.Interfaces;
using Yape.TransactionService.Domain.Transactions;
using Yape.TransactionService.Infrastructure.Common.Persistence;

namespace Yape.TransactionService.Infrastructure.Transactions.Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _dbContext;
        public TransactionRepository(TransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> CreateFinancialTransactionAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.CommitChangesAsync();

            return transaction.Id;
        }

        public async Task<Transaction> UpdateTransactionStatus(Guid id, string status)
        {
            Transaction transaction;

            var transactions = _dbContext.Transactions.ToList();

            try
            {
                transaction = await _dbContext.Transactions.Where(t => t.Id == id)
                      .SingleAsync();
            }
            catch (Exception)
            {
                throw;
            }

            if (transaction is null)
            {
                throw new KeyNotFoundException($"Transaction with ID {id} not found.");
            }

            transaction!.Status = status;
            transaction.CreatedAt = DateTime.UtcNow;
            _dbContext.Transactions.Update(transaction);
            await _dbContext.CommitChangesAsync();

            return transaction;
        }
    }
}
