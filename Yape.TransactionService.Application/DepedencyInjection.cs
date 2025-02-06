using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Yape.TransactionService.Domain.Kafka;

namespace Yape.TransactionService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {          
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

            return services;
        }
    }
}