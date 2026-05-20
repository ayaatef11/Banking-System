namespace Application.Features.Transactions.Withdraw;

public sealed class WithdrawTransactionCommandHandler(IValidationService validationService,IAccountService accountService,ITransactionService transactionService)
    : ICommandHandler<WithdrawTransactionCommand, TransactionResponse>
{
    public async Task<Result<TransactionResponse>> Handle(WithdrawTransactionCommand command,CancellationToken cancellationToken)
    {
        Account? account = await accountService.GetAccountByIdAsync(command.AccountId, cancellationToken);

        if (account == null)
        {
            return Result.Failure<TransactionResponse>(AccountError.NotFound(accountId: command.AccountId));
        }

        Result validationResult = validationService.ValidateWithdrawAmount(account.Balance, command.Amount);

        if (validationResult.IsFailure)
        {
            return Result.Failure<TransactionResponse>(validationResult.Error);
        }

        Result updateResult = accountService.Withdraw(account, command.Amount);

        if (updateResult.IsFailure)
        {
            return Result.Failure<TransactionResponse>(updateResult.Error);
        }

        return await transactionService.ProcessWithdrawalAsync(account, command.Amount, cancellationToken);
    }
}
