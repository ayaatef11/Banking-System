using Domain.Accounts;
using Domain.Extensions;

namespace Banking_System.Mappings;
public class AccountResponseMapper : IMapper<Account, AccountResponse>
{
    public AccountResponse Map(Account account)
    {
        return new AccountResponse(account.Id,
                    account.OwnerName,
                    account.AccountType.GetDisplayName(),
                    account.Balance,
                    account.AccountNumber,
                    account.CreatedAt);
    }
}
