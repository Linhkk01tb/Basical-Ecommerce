using Demo.DTOs.Category;
using Demo.Helpers.QueryObjects;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync(CategoryQueryObject queryObject);

        Task<Category?> GetCategoryByIdAsync(int categoryId);
        
        Task<Category> CreateCategoryAsync(CreateCategoryDTO categoryDTO);
        
        Task<Category?> UpdateCategoryAsync(int catetogryId, UpdateCategoryDTO categoryDTO);
        
        Task<Category?> DeleteCategoryAsync(int categoryId);
        
        Task<bool> HasCategoryAsync(int categoryId);
    }
}
