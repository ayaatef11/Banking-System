using Application.Common.Mappings;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Shared;

namespace Infrastructure.Services;

public class TransactionService(IApplicationDbContext context, IMapper<Transaction,TransactionResponse>mapper,ILogger logger): ITransactionService
{
    public async Task<Result<TransactionResponse>> ProcessDepositAsync(Account account, decimal amount,
        CancellationToken cancellationToken)
    {
        IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var deposit = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Type = TransactionType.Deposit,
                Amount = amount,
                TargetAccountNumber = account.AccountNumber,
                CreatedAt = DateTime.UtcNow
            };
            context.Transactions.Add(deposit);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            logger.Information("Deposit successful: Account {AccountId}, Amount {Amount}", account.Id, amount);

            TransactionResponse result = mapper.Map(deposit);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            logger.Error(ex,
                "Failed to process deposit for account {AccountId}: {Message}",
                account.Id,ex.Message);

            return Result.Failure<TransactionResponse>(TransactionError.Failed(ex.Message));
        }
    }

    public async Task<Result<TransactionResponse>> ProcessWithdrawalAsync(Account account, decimal amount, CancellationToken cancellationToken)
    {
        IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var withdrawTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Type = TransactionType.Withdraw,
                Amount = amount,
                TargetAccountNumber = account.AccountNumber,
                CreatedAt = DateTime.UtcNow
            };

            account.Balance -= amount;
            context.Transactions.Add(withdrawTransaction);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            logger.Information(
                "Withdrawal successful: Account {AccountId}, Amount {Amount}, Remaining Balance {Balance}",
                account.Id,
                amount,
                account.Balance);

            TransactionResponse response = mapper.Map(withdrawTransaction);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            logger.Error(ex,"Failed to process withdrawal for account {AccountId}: {Message}",
                account.Id,ex.Message);

            await transaction.RollbackAsync(cancellationToken);

            return Result.Failure<TransactionResponse>(TransactionError.Failed(ex.Message));
        }
    }
}
