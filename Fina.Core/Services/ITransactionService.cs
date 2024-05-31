using Fina.Core.Responses;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;

namespace Fina.Core.Services;

public interface ITransactionService
{
    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
    
    Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    
    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
    
    Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
}