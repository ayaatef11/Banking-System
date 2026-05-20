namespace Application.Features.Accounts.Create;

public sealed class CreateAccountCommand : ICommand<AccountResponse>
{
    public string OwnerName { get; set; }
    public AccountType AccountType { get; set; } = AccountType.Savings;
};
