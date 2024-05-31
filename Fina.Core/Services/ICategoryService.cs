using Fina.Core.Responses;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;

namespace Fina.Core.Services;

public interface ICategoryService
{
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
    
    Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    
    Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request);
}