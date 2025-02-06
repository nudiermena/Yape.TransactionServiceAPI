using Confluent.Kafka;
using MassTransit;
using Yape.TransactionService.Api.Extensions;
using Yape.TransactionService.Application;
using Yape.TransactionService.Consumers;
using Yape.TransactionService.Infrastructure;
using Yape.TransactionService.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    // Main bus configuration
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
  
    x.AddRider(rider =>
    {
        rider.AddConsumer<TransactionStatusUpdatedConsumer>();
        rider.AddProducer<TransactionCreatedEvent>("transaction-created");
        rider.UsingKafka((context, k) =>
        {
            k.ClientId = "transaction-service";
            k.Host("localhost:29092");
            
            k.TopicEndpoint<TransactionUpdatedEvent>(
            "transaction-created",
            "transaction-service-group",
           e =>
           {
               e.UseMessageRetry(retry => retry.Interval(1000, TimeSpan.FromSeconds(1)));
               e.ConfigureConsumer<TransactionStatusUpdatedConsumer>(context);
               e.AutoOffsetReset = AutoOffsetReset.Earliest;
           });
        });
    });
});

var app = builder.Build();

var busControl = app.Services.GetRequiredService<IBusControl>();
await busControl.StartAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.ApplyMigrations();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
