using Fina.Api.Common.Api;
using Fina.Core.Services;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", ServiceAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transação")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> ServiceAsync(ITransactionService service, CreateTransactionRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        var result = await service.CreateAsync(request);

        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
    }
}