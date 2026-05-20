namespace Application.Features.Transactions.Deposit;

public sealed record DepositTransactionCommand(Guid AccountId, decimal Amount) : ICommand<TransactionResponse>;
