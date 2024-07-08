using Demo.DTOs;
using Demo.Interfaces;
using Demo.Repositories;
using Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public ImagesController(IProductRepository productRepository, IImageRepository imageRepository, IImageService imageService)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        [HttpGet("ImagesByProduct/{productId:guid}")]
        public async Task<IActionResult> GetAllByProduct(Guid productId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var product = await _productRepository.GetProductsByIdAsync(productId);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                var images = await _imageRepository.GetAllImageByProductAsync(productId);
                return StatusCode(StatusCodes.Status200OK, images);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet("{imageId:guid}")]
        public async Task<IActionResult> GetById(Guid imageId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var image = await _imageRepository.GetImageByIdAsync(imageId);
                if (image == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return File(await _imageService.GetImageAsStreamAsync(imageId), "img/png",image);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddImageToProduct([FromForm]Guid productId, IFormFile imageDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var product = await _productRepository.GetProductsByIdAsync(productId);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                var image = await _imageService.UploadFileImageToProduct(productId, imageDTO);
                return StatusCode(StatusCodes.Status200OK, image);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut("{imageId:guid}")]
        public async Task<IActionResult> UpdateAvatar(Guid imageId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                await _imageRepository.UpdateAvatarAsync(imageId);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> RemoveImage(Guid imageId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var image = await _imageRepository.RemoveImageAsync(imageId);
                return StatusCode(StatusCodes.Status200OK, image);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
