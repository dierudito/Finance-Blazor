using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Response;
using FinaFlow.Core.Models;

namespace FinaFlow.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPut("/{id:long}", HandleAsync)
        .WithName("Transação: Atualizar")
        .WithSummary("Atualiza uma transaçào")
        .WithDescription("Atualiza uma transaçào")
        .WithOrder(2)
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        long id,
        UpdateTransactionRequest request)
    {
        request.Id = id;
        request.UserId = ApiConfigurations.UserId;
        var response = await handler.UpdateAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}