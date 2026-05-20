namespace Application.Common.Interfaces;

public interface ITransactionService
{
    Task<Result<TransactionResponse>> ProcessDepositAsync(Account account, decimal amount, CancellationToken cancellationToken);

    Task<Result<TransactionResponse>> ProcessWithdrawalAsync(Account account, decimal amount,CancellationToken cancellationToken);
}
