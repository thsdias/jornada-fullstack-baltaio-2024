using Fina.Api.Common.Api;
using Fina.Core.Services;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", ServiceAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

    private static async Task<IResult> ServiceAsync(ICategoryService service, UpdateCategoryRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var result = await service.UpdateAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}