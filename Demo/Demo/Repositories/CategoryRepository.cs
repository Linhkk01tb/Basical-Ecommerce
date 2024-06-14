using Demo.Data;
using Demo.DTOs.Category;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Mapper;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DemoDbContext _context;

        public CategoryRepository(DemoDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateCategoryAsync(CreateCategoryDTO categoryDTO)
        {
            var newCategory = new Category
            {
                CategoryName = categoryDTO.CategoryName,
                CreatedDate = DateTime.UtcNow.ToLocalTime(),
                ModifiedDate = DateTime.UtcNow.ToLocalTime()
            };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category?> DeleteCategoryAsync(int categoryId)
        {
            var deleteCategory = await _context.Categories!.SingleOrDefaultAsync(ct => ct.CategoryId == categoryId);
            if (deleteCategory == null)
                return null;
            _context.Categories.Remove(deleteCategory);
            await _context.SaveChangesAsync();
            return deleteCategory;
        }

        public async Task<List<Category>> GetAllCategoryAsync(CategoryQueryObject queryObject)
        {
            var categories = _context.Categories.Include(ct => ct.Products).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.CategoryName))
            {
                categories = categories.Where(ct => ct.CategoryName.Contains(queryObject.CategoryName));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy)
                {
                    case "CategoryName":
                        categories = queryObject.IsDescending ? categories.OrderByDescending(ct => ct.CategoryName) : categories.OrderBy(ct => ct.CategoryName);
                        break;
                    default:
                        categories = categories.OrderByDescending(ct => ct.CreatedDate);
                        break;
                }
            }

            var categoriesRes = await categories.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToListAsync();

            return categoriesRes.ToList();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            var categoryById = await _context.Categories!.Include(ct => ct.Products).FirstOrDefaultAsync(ct => ct.CategoryId == categoryId);
            if (categoryById == null)
                return null;
            return categoryById;
        }

        public async Task<bool> HasCategoryAsync(int categoryId)
        {
            return await _context.Categories!.AnyAsync(ct => ct.CategoryId == categoryId);
        }

        public async Task<Category?> UpdateCategoryAsync(int catetogryId, UpdateCategoryDTO categoryDTO)
        {
            var updateCategory = await _context.Categories!.SingleOrDefaultAsync(ct => ct.CategoryId == catetogryId);
            if (updateCategory == null)
                return null;
            updateCategory.CategoryName = categoryDTO.CategoryName;
            updateCategory.ModifiedDate = DateTime.UtcNow.ToLocalTime();
            _context.Categories.Update(updateCategory);
            await _context.SaveChangesAsync();
            return updateCategory;
        }
    }
}
