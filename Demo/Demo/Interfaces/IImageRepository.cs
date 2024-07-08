using Demo.DTOs;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface IImageRepository
    {
        Task AddImageAsync(Image image);
        //Task<ImageDTO> AddImageAsync(Image image);
        Task<List<ImageDTO>> GetAllImageByProductAsync(Guid productId);
        Task<string?> GetImagePathByIdAsync(Guid imageId);
        Task<string?> GetImageByIdAsync(Guid imageId);

        Task<string> RemoveImageAsync(Guid imageId);

        Task UpdateAvatarAsync(Guid imageId);
    }
}
