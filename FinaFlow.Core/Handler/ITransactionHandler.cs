using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Response;

namespace FinaFlow.Core.Handler;

public interface ITransactionHandler
{
    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
    Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest request);
    Task<PagedResponse<List<Transaction?>>> GetByPeriodAsync(GetByPeriodTransactionRequest request);
}