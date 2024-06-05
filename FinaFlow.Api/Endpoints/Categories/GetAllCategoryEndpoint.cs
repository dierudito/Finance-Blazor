using FinaFlow.Api.Common.Api;
using FinaFlow.Core;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Categories;

public class GetAllCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("/", HandleAsync)
        .WithName("Categoria: Obter Todos")
        .WithSummary("Recupera todas as categorias")
        .WithDescription("Recupera todas as categorias")
        .WithOrder(5)
        .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configurations.DefaultPageNumber,
        [FromQuery] int pageSize = Configurations.DefaultPageSize)
    {
        var request = new GetAllCategoryRequest
        {
            UserId = ApiConfigurations.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var response = await handler.GetAllAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}