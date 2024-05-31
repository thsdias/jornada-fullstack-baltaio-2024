using Fina.Api.Common.Api;
using Fina.Core.Services;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", ServiceAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Recupera uma categoria")
            .WithDescription("Recupera uma categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();

    private static async Task<IResult> ServiceAsync(ICategoryService service, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId =  ApiConfiguration.UserId,
            Id = id
        };

        var result = await service.GetByIdAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}