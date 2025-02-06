using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Yape.TransactionService.Domain.Transactions;

namespace Yape.TransactionService.Infrastructure.Common.Persistence
{
    public sealed class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .ToTable("transactions");
        }
    }
}