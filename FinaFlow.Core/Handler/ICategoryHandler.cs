using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Response;

namespace FinaFlow.Core.Handler;

public interface ICategoryHandler
{
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest request);
    Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoryRequest request);
}