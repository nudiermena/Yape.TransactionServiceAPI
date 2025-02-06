
namespace Yape.TransactionService.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorker(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

         //   // Register MediatR
         //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<TransactionStatusUpdateCommand>());


            return services;
        }
    }
}
