using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Response;
using FinaFlow.Core.Models;
using Microsoft.AspNetCore.Mvc;
using FinaFlow.Core;

namespace FinaFlow.Api.Endpoints.Transactions;

public class GetByPeriodTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("/", HandleAsync)
        .WithName("Transação: Obter por período")
        .WithSummary("Obtém transações por período")
        .WithDescription("Obtém transações por período")
        .WithOrder(5)
        .Produces<PagedResponse<List<Transaction>?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configurations.DefaultPageNumber,
        [FromQuery] int pageSize = Configurations.DefaultPageSize)
    {
        var request = new GetByPeriodTransactionRequest
        {
            EndDate = endDate,
            StartDate = startDate,
            UserId = ApiConfigurations.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var response = await handler.GetByPeriodAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}