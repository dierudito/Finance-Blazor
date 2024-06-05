using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Response;
using FinaFlow.Core.Models;

namespace FinaFlow.Api.Endpoints.Transactions;

public class GetByIdTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("/{id:long}", HandleAsync)
        .WithName("Transação: Obter")
        .WithSummary("Obter uma transaçào")
        .WithDescription("Obter uma transaçào")
        .WithOrder(4)
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        long id)
    {
        var request = new GetByIdTransactionRequest
        {
            Id = id,
            UserId = ApiConfigurations.UserId
        };
        var response = await handler.GetByIdAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}