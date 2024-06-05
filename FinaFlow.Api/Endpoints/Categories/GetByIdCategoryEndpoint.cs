using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Response;

namespace FinaFlow.Api.Endpoints.Categories;

public class GetByIdCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("/{id}", HandleAsync)
        .WithName("Categoria: Obter Única")
        .WithSummary("Obtém uma categoria")
        .WithDescription("Obtém uma categoria")
        .WithOrder(4)
        .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        long id)
    {
        var request = new GetByIdCategoryRequest
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