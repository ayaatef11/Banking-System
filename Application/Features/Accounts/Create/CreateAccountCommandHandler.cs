using Application.Common.Mappings;

namespace Application.Features.Accounts.Create;

public sealed class CreateAccountCommandHandler(IApplicationDbContext context,IMapper<Account,AccountResponse>mapper,IAccountNumberGenerator accountNumberGenerator) : ICommandHandler<CreateAccountCommand, AccountResponse>
{
    public async Task<Result<AccountResponse>> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    { 
        var account = new Account
        {
            Id = Guid.NewGuid(),
            OwnerName = command.OwnerName,
            AccountNumber = accountNumberGenerator.Generate(),
            AccountType = command.AccountType,
            Balance = AccountConstants.MinBalance,
            CreatedAt = DateTime.UtcNow
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync(cancellationToken);
        AccountResponse response = mapper.Map(account);

        return Result.Success(response);
    }
}
