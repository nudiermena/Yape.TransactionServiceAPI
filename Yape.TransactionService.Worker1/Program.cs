using Confluent.Kafka;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Yape.TransactionService.Consumers;
using Yape.TransactionService.Worker;
using Yape.TransactionService.Worker.Yape.AntiFraudService.Worker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddMassTransit(x =>
        {
            // we aren't using the bus, only Kafka
            x.UsingInMemory();

            x.AddRider(r =>
            {
                r.AddConsumer<TransactionStatusUpdatedConsumer>();

                r.UsingKafka((context, cfg) =>
                {

                    //cfg.Host(context);
                    cfg.Host("localhost:29092");

                    //cfg.ClientId = "transaction-service1";                   

                    cfg.TopicEndpoint<string, TransactionUpdatedEvent>("transaction-antifraudchecked", "antifraud-service-group", e =>
                    {
                        e.AutoOffsetReset = AutoOffsetReset.Earliest;
                        e.ConcurrentMessageLimit = 1;

                        e.UseRetryConfiguration();

                        e.ConfigureConsumer<TransactionStatusUpdatedConsumer>(context);
                    });                    
                });
            });
        });
    })
    .Build();

await host.RunAsync();