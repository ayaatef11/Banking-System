using Domain.Extensions;
using Domain.Transactions;
using Web.Api.Endpoints.Transactions;

namespace Banking_System.Mappings
{
    public class TransactionResponseMapper : IMapper<Transaction, TransactionResponse>
    {
        public TransactionResponse Map(Transaction source)
        {
            return new TransactionResponse(
                source.Id,
                source.AccountId,
                source.Type.GetDisplayName(),
                source.Amount,
                source.TargetAccountNumber,
                source.CreatedAt);
        }
    }
}
