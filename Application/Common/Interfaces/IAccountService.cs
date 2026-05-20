namespace Application.Common.Interfaces;

public interface IAccountService
{
    Task<Account?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken);
    Result Withdraw(Account account, decimal amount);
}
