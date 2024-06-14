using Demo.DTOs;

namespace Demo.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadFileImageToProduct(Guid productId, IFormFile imageDTO);
        //Task<ImageDTO> UploadFileImageToProduct(Guid productId, IFormFile imageDTO);
    }
}
