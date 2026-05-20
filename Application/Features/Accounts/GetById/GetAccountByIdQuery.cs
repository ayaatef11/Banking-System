namespace Application.Features.Accounts.GetById;

public sealed record GetAccountByIdQuery(Guid AccountId) : IQuery<AccountResponse>;
