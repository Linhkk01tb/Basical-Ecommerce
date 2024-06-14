using Demo.DTOs.Category;
using Demo.Helpers;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Mapper;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoryQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var categories = await _categoryRepository.GetAllCategoryAsync(queryObject);
            var categoriesRes = categories.Select(s => s.ToCategoryDTO()).ToList();
            return StatusCode(StatusCodes.Status200OK, new
            {
                pageNumber = queryObject.PageNumber,
                pageSize = queryObject.PageSize,
                categoriesRes
            }); ;
        }
        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var categoryById = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (categoryById == null)
                return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, categoryById.ToCategoryDTO());
        }
        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Create(CreateCategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var newCategory = await _categoryRepository.CreateCategoryAsync(categoryDTO);
            var category = newCategory.ToCategoryDTO();
            return StatusCode(StatusCodes.Status201Created, new
            {
                IsCreated = true,
                category
            });
        }

        [HttpPut("{categoryId:int}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Update(int categoryId, UpdateCategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var updateCategory = await _categoryRepository.UpdateCategoryAsync(categoryId, categoryDTO);
            if (updateCategory == null)
                return StatusCode(StatusCodes.Status404NotFound);
            var category = updateCategory.ToCategoryDTO();
            return StatusCode(StatusCodes.Status200OK, new
            {
                IsUpdated = true,
                category
            });
        }
        [HttpDelete("{categoryId:int}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Delete(int categoryId)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var deleteCategory = await _categoryRepository.DeleteCategoryAsync(categoryId);
            if (deleteCategory == null)
                return StatusCode(StatusCodes.Status404NotFound);
            var category = deleteCategory.ToCategoryDTO();
            return StatusCode(StatusCodes.Status200OK, new
            {
                IsDeleted = true,
                category
            });
        }
    }
}
