using Demo.DTOs;
using Demo.Models;

namespace Demo.Mappers
{
    public static class ImageMapper
    {
        public static ImageDTO ToImageDTO(this Image image)
        {
            return new ImageDTO
            {
                ImageId = image.ImageId,
                ImageName = image.ImageName,
                ImageUrl = image.ImageUrl,
                IsAvatar = image.IsAvatar
            };
        }
    }
}
