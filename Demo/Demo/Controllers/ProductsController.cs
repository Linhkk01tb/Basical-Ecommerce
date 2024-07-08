using Demo.DTOs.Product;
using Demo.Helpers;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Mapper;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    public class ProductsController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var products = await _productRepository.GetAllProductsAsync(queryObject);
            var productsRes = products.Select(s => s.ToProductDTO()).ToList();
            return StatusCode(StatusCodes.Status200OK, new
            {
                total = await _productRepository.CountProductAsync(),
                pageNumber = queryObject.PageNumber,
                pageSize = queryObject.PageSize,
                productsRes
            });
        }

        [HttpGet("{productId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid productId)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var productById = await _productRepository.GetProductsByIdAsync(productId);

            if (productById == null)
                return StatusCode(StatusCodes.Status404NotFound);
            var product = productById.ToProductDTO();
            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost("{categoryId:int}")]

        public async Task<IActionResult> Create(int categoryId, CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            if (!await _categoryRepository.HasCategoryAsync(categoryId))
                return StatusCode(StatusCodes.Status400BadRequest, "Category does not exist!");
            var newProduct = await _productRepository.CreateProductAsync(productDTO.ToCreateProduct(categoryId));
            var product = newProduct.ToProductDTO();
            return StatusCode(StatusCodes.Status201Created, new
            {
                IsCreated = true,
                product
            });
        }

        [HttpPut("{productId:guid}")]

        public async Task<IActionResult> Update(Guid productId, UpdateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var updateProduct = await _productRepository.UpdateProductAsync(productId, productDTO);
            if (updateProduct == null)
                return StatusCode(StatusCodes.Status404NotFound);
            var product = updateProduct.ToProductDTO();
            return StatusCode(StatusCodes.Status200OK, new
            {
                IsUpdated = true,
                product
            });
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var deleteProduct = await _productRepository.DeleteProductAsync(productId);
            if (deleteProduct == null)
                return StatusCode(StatusCodes.Status404NotFound);
            var product = deleteProduct.ToProductDTO();
            return StatusCode(StatusCodes.Status200OK, new
            {
                IsDeleted = true,
                product
            });
        }
    }
}
