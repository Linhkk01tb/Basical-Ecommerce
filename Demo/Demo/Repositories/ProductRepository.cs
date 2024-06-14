using Demo.Data;
using Demo.DTOs.Product;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Mapper;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemoDbContext _context;

        public ProductRepository(DemoDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedDate = DateTime.UtcNow.ToLocalTime();
            product.ModifiedDate = DateTime.UtcNow.ToLocalTime();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(Guid productId)
        {
            var deleteProduct = await _context.Products!.SingleOrDefaultAsync(pd => pd.ProductId == productId);
            if (deleteProduct == null)
                return null;
            _context.Products.Remove(deleteProduct);
            await _context.SaveChangesAsync();
            return deleteProduct;
        }

        public async Task<List<Product>> GetAllProductsAsync(ProductQueryObject queryObject)
        {
            var products = _context.Products!.Include(pd=>pd.Images).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.ProductName))
            {
                products = products.Where(pd => pd.ProductName.Contains(queryObject.ProductName));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.ProductPriceFrom.ToString()))
            {
                products = products.Where(pd => pd.ProductPrice >= queryObject.ProductPriceFrom);
            }
            if (!string.IsNullOrWhiteSpace(queryObject.ProductPriceTo.ToString()))
            {
                products = products.Where(pd => pd.ProductPrice <= queryObject.ProductPriceTo);
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy)
                {
                    case "ProductName":
                        products = queryObject.IsDescending ? products.OrderByDescending(pd => pd.ProductName) : products.OrderBy(pd => pd.ProductName);
                        break;
                    case "ProductPrice":
                        products = queryObject.IsDescending ? products.OrderByDescending(pd => pd.ProductPrice) : products.OrderBy(pd => pd.ProductPrice);
                        break;
                    case "CreatedDate":
                        products = queryObject.IsDescending ? products.OrderByDescending(pd => pd.CreatedDate) : products.OrderBy(pd => pd.CreatedDate);
                        break;
                    default:
                        products = products.OrderByDescending(pd => pd.CreatedDate);
                        break;
                }
            }

            return await products.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Product?> GetProductsByIdAsync(Guid productId)
        {
            var productById = await _context.Products!.Include(pd=>pd.Images).SingleOrDefaultAsync(pd => pd.ProductId == productId);
            if (productById == null)
                return null;
            return productById;
        }

        public async Task<bool> HasAvatarAsync(Guid productId)
        {
            return await _context.Images.AnyAsync(im => im.ProductId == productId && im.IsAvatar == true);
        }

        public async Task<Product?> UpdateProductAsync(Guid productId, UpdateProductDTO productDTO)
        {
            var updateProduct = await _context.Products!.SingleOrDefaultAsync(pd => pd.ProductId == productId);
            if (updateProduct == null)
                return null;
            updateProduct.ProductName = productDTO.ProductName;
            updateProduct.ProductPrice = productDTO.ProductPrice;
            updateProduct.ProductQuantity = productDTO.ProductQuantity;
            updateProduct.ProductDescription = updateProduct.ProductDescription ?? "";
            updateProduct.ModifiedDate = DateTime.UtcNow.ToLocalTime();
            _context.Products.Update(updateProduct);
            await _context.SaveChangesAsync();
            return updateProduct;
        }
    }
}
