namespace Web.Api.Mappings;

internal sealed class CreateAccountRequestMapper : IMapper<CreateAccountRequest, CreateAccountCommand>
{
    public CreateAccountCommand Map(CreateAccountRequest request)
    {
        return new CreateAccountCommand
        {
            OwnerName = request.OwnerName,
            AccountType = request.AccountType
        };
    }
}
