using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Response;

namespace FinaFlow.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapDelete("/{id}", HandleAsync)
        .WithName("Categoria: Delete")
        .WithSummary("Exclui uma categoria")
        .WithDescription("Exclui uma categoria")
        .WithOrder(3)
        .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        { 
            Id = id,
            UserId = ApiConfigurations.UserId
        };
        var response = await handler.DeleteAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}