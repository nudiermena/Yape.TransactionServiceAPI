using MassTransit;
using MediatR;
using Yape.TransactionService.Consumers;
using Yape.TransactionService.Contracts.Transactions.Commands;
using Yape.TransactionService.Domain.Transactions;

namespace Yape.TransactionService.Worker
{
    public class TransactionStatusUpdatedConsumer : IConsumer<TransactionUpdatedEvent>
    {
        private readonly IMediator _mediator;
        public TransactionStatusUpdatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            var transactionStatusUpdateCommand = new TransactionStatusUpdateCommand(context.Message.TransactionId, context.Message.Status);
            var response = await _mediator.Send(transactionStatusUpdateCommand);

            var transcId = context.Message.TransactionId;
            Console.WriteLine($"Transaction ${context.Message.TransactionId} was updated succesfully");
           await Task.CompletedTask;
        }

        private static YapeTransactionStatus GetStatus(string status)
        {
            _ = Enum.TryParse(status, out YapeTransactionStatus statusValue);

            return statusValue;
        }
    }
}
