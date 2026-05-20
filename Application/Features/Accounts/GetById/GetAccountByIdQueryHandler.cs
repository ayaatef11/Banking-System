using Application.Common.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts.GetById;

public sealed class GetAccountByIdQueryHandler(IApplicationDbContext context,IMapper<Account,AccountResponse>mapper): IQueryHandler<GetAccountByIdQuery, AccountResponse>
{
    public async Task<Result<AccountResponse>> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        Account? account = await context.Accounts
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == query.AccountId, cancellationToken);

        if (account is null)
        {
            return Result.Failure<AccountResponse>(AccountError.NotFound(query.AccountId));
        }

        AccountResponse response = mapper.Map(account);

        return response;
    }
}
