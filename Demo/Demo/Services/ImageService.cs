using Demo.DTOs;
using Demo.Interfaces;
using Demo.Mappers;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Demo.Services
{
    public class ImageService : IImageService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;

        public ImageService(IProductRepository productRepository, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }

        public async Task<Stream> GetImageAsStreamAsync(Guid imageId)
        {
            return new MemoryStream(await ConvertImageToByteArray(imageId));
        }

        public async Task<string> UploadFileImageToProduct(Guid productId, IFormFile image)
        {
            var product = await _productRepository.GetProductsByIdAsync(productId);
            if (product == null)
                throw new Exception("Product does not exist!");
            var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", image.FileName);
            using (var stream = new FileStream(fullpath, FileMode.OpenOrCreate))
            {
                await image.CopyToAsync(stream);
            }
            var newImage = new Image
            {
                ImageName = image.FileName,
                ImageUrl = "/Images/" + image.FileName,
                IsAvatar = true,
                ProductId = productId
            };

            if (await _productRepository.HasAvatarAsync(productId))
                newImage.IsAvatar = false;
            await _imageRepository.AddImageAsync(newImage);
            return "Added image to product!";
        }

        private async Task<byte[]> ConvertImageToByteArray(Guid imageId)
        {
            var path = await _imageRepository.GetImagePathByIdAsync(imageId);
            if (path == null)
                return null;
            return File.ReadAllBytes(path);
        }
    }
}
