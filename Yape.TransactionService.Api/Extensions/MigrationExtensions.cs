using Microsoft.EntityFrameworkCore;
using Yape.TransactionService.Infrastructure.Common.Persistence;

namespace Yape.TransactionService.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using TransactionDbContext dbContext = scope.ServiceProvider.GetRequiredService<TransactionDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
