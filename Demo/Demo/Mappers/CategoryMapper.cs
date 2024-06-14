using Demo.DTOs.Category;
using Demo.Mapper;
using Demo.Models;

namespace Demo.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate,
                Products = category.Products.Select(s => s.ToProductDTO()).ToList(),
            };
        }
        public static Category ToCreateCategoryDTO(this CreateCategoryDTO categoryDTO)
        {
            return new Category
            {
                CategoryName = categoryDTO.CategoryName,
            };
        }
    }
}
