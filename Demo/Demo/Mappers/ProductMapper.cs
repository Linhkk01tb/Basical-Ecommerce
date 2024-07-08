using Demo.DTOs.Product;
using Demo.Mappers;
using Demo.Models;

namespace Demo.Mapper
{
    public static class ProductMapper
    {
        public static ProductDTO ToProductDTO(this Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductQuantity = product.ProductQuantity,
                ProductDescription = product.ProductDescription ?? "",
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate,
                Images = product.Images.Select(s=>s.ToImageDTO()).ToList(),
            };
        }
        public static Product ToCreateProduct(this CreateProductDTO productDTO, int categoryId)
        {
            return new Product
            {
                ProductName = productDTO.ProductName,
                ProductPrice = productDTO.ProductPrice,
                ProductQuantity = productDTO.ProductQuantity,
                ProductDescription = productDTO.ProductDescription ?? "",
                CategoryId = categoryId,
            };
        }
        public static Product ToUpdateProduct(this UpdateProductDTO productDTO)
        {
            return new Product
            {
                ProductName = productDTO.ProductName,
                ProductPrice = productDTO.ProductPrice,
                ProductQuantity = productDTO.ProductQuantity,
                ProductDescription = productDTO.ProductDescription ?? ""
            };
        }
    }
}
