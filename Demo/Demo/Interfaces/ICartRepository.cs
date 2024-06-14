using Demo.DTOs.Cart;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItemDTO>> GetAllCartItemAsync(string cartId);
        Task<string> GetCartIdByUserAsync(AppUser user);
        Task CreateCartAsync(AppUser user);
        Task<string> AddToCartAsync(Guid cartId, Guid productId, int buyQuanlity = 1);
        Task<string> RemoveFromCartAsync(Guid cartId, Guid productId);
    }
}
