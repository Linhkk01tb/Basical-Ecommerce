using Demo.Data;
using Demo.DTOs;
using Demo.Interfaces;
using Demo.Mappers;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DemoDbContext _context;

        public ImageRepository(DemoDbContext context)
        {
            _context = context;
        }
        public async Task AddImageAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ImageDTO>> GetAllImageByProductAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception("Product does not exist!");
            return await _context.Images.Where(im => im.ProductId == productId).Select(s=>s.ToImageDTO()).ToListAsync();
        }

        public async Task<string?> GetImagePathByIdAsync(Guid imageId)
        {
            var image = await _context.Images!.FindAsync(imageId);
            if (image == null)
                return null;
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", image.ImageName);
        }
        public async Task<string?> GetImageByIdAsync(Guid imageId)
        {
            var image = await _context.Images!.FindAsync(imageId);
            if (image == null)
                return null;
            return image.ImageName;
        }
        public async Task<string> RemoveImageAsync(Guid imageId)
        {
            var deleteImage = await _context.Images.SingleOrDefaultAsync(im => im.ImageId == imageId);
            if (deleteImage == null)
                throw new Exception("Image does not exist!");
            if (deleteImage.IsAvatar)
                throw new Exception("Can't delete the avatar!");
            _context.Images.Remove(deleteImage);
            await _context.SaveChangesAsync();
            return "Đã xóa ảnh " + deleteImage.ImageName + " khỏi sản phẩm!";
        }

        public async Task UpdateAvatarAsync(Guid imageId)
        {
            var image = await _context.Images.SingleOrDefaultAsync(im => im.ImageId == imageId);
            if (image == null)
                throw new Exception("Image does not exist!");
            if (image.IsAvatar)
                throw new Exception("This image was avatar!");
            image.IsAvatar = true;
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
        }
    }
}
