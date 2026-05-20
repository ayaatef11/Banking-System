namespace Application.Features.Accounts.Create;

public sealed class CreateAccountCommandHandler(IApplicationDbContext context,IAccountNumberGenerator accountNumberGenerator,
    TimeProvider timeProvider,
    IGuidGenerator guidGenerator) : ICommandHandler<CreateAccountCommand, AccountResponse>
{
    public async Task<Result<AccountResponse>> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.OwnerName))
        {
            return Result.Failure<AccountResponse>(AccountError.Required(nameof(command.OwnerName)));
        }

        if (!Enum.IsDefined(command.AccountType))
        {
            return Result.Failure<AccountResponse>(AccountError.Required(nameof(command.AccountType)));
        }

        var account = new Account
        {
            Id = guidGenerator.NewGuid(),
            OwnerName = command.OwnerName,
            AccountNumber = accountNumberGenerator.Generate(),
            AccountType = command.AccountType,
            Balance = AccountConstants.MinBalance,
            CreatedAt = timeProvider.GetUtcNow()
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync(cancellationToken);

        var response = new AccountResponse(
            account.Id,
            account.OwnerName,
            account.AccountType.GetDisplayName(),
            account.Balance,
            account.AccountNumber,
            account.CreatedAt
        );

        return Result.Success(response);
    }
}
