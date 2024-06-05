using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace FinaFlow.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPut("/{id}", HandleAsync)
        .WithName("Categoria: Atualizar")
        .WithSummary("Atualiza uma categoria")
        .WithDescription("Atualiza uma categoria")
        .WithOrder(2)
        .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [FromRoute] long id,
        [FromBody] UpdateCategoryRequest request)
    {
        request.UserId = ApiConfigurations.UserId;
        request.Id = id;
        var response = await handler.UpdateAsync(request);
        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}