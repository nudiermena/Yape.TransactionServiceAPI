using Confluent.Kafka;
using Yape.AntiFraudService.Worker.Extensions;
using Yape.TransactionService.Worker;
using Yape.TransactionService.Consumers;
using Yape.TransactionService.Application;
using Yape.TransactionService.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplication();
        services.AddInfrastructure(hostContext.Configuration);
        services.AddWorker();

        services.AddMassTransit(x =>
        {
            // we aren't using the bus, only Kafka
            x.UsingInMemory();

            x.AddRider(r =>
            {
                r.AddProducer<TransactionCreatedEvent>("transaction-created");
                r.AddConsumer<TransactionStatusUpdatedConsumer>();
                
                r.UsingKafka((context, cfg) =>
                {
                    //cfg.Host(context);
                    cfg.Host("localhost:29092");
                    cfg.ClientId = "transaction-service";                

                    cfg.TopicEndpoint<string, TransactionUpdatedEvent>("transaction-antifraudchecked", "antifraud.transaction.worker", e =>
                    {
                        e.AutoOffsetReset = AutoOffsetReset.Earliest;
                        e.ConcurrentMessageLimit = 1;

                        e.RetryConfiguration();

                        e.ConfigureConsumer<TransactionStatusUpdatedConsumer>(context);
                    });                    
                });
            });
        });
    })
    .Build();

await host.RunAsync();