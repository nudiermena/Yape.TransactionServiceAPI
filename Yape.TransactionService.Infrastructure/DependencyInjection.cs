using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yape.TransactionService.Application.Common.Interfaces;
using Yape.TransactionService.Infrastructure.Common.Persistence;
using Yape.TransactionService.Infrastructure.Transactions.Persistence;

namespace Yape.TransactionService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

            string? connectionString = configuration.GetConnectionString("YapeConnectionString");

            services.AddDbContext<TransactionDbContext>(options =>
                options.UseNpgsql(connectionString, options =>
                {
                }));

            services.AddTransient<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
