using Demo.DTOs.Product;
using Demo.Helpers.QueryObjects;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(ProductQueryObject queryObject);

        Task<Product?> GetProductsByIdAsync(Guid productId);

        Task<Product> CreateProductAsync(Product productDTO);

        Task<Product?> UpdateProductAsync(Guid productId,UpdateProductDTO productDTO);

        Task<Product?> DeleteProductAsync(Guid productId);

        Task<bool> HasAvatarAsync(Guid productId);

        Task<int> CountProductAsync();
    }
}
