using MediatR;

namespace Web.Api.Endpoints.Accounts;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/accounts",
                async (CreateAccountRequest request, ISender sender,
                    IMapper<CreateAccountRequest, CreateAccountCommand> mapper, CancellationToken cancellationToken) =>
                {
                    CreateAccountCommand command = mapper.Map(request);

                    Result<AccountResponse> result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Ok, CustomResults.Problem);
                })
            //.HasApiVersion(1.0)
            .Produces<AccountResponse>()
            .WithSummary("Create a new account")
            .WithDescription("Creates a new account with the specified owner name and account type.")
            .WithTags(Tags.Accounts);
    }
}
