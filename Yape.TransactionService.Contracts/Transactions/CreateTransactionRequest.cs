namespace Yape.TransactionService.Contracts.Transactions
{
    public record CreateTransactionRequest(Guid SourceAccountId, Guid TargetAccountId, int TransferTypeId, decimal Value);   
}
