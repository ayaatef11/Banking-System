using Application.Common.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts.Get;

public sealed class GetAccountQueryHandler(IApplicationDbContext context, IMapper<Account, AccountResponse> mapper) : IQueryHandler<GetAccountQuery, List<AccountResponse>>
{
    public async Task<Result<List<AccountResponse>>> Handle(GetAccountQuery query, CancellationToken cancellationToken)
    {
        List<Account> accounts = await context.Accounts.AsNoTracking()
     .ToListAsync(cancellationToken);

        List<AccountResponse> result = accounts.Select(mapper.Map)
            .ToList();

        return result;
    }
}
