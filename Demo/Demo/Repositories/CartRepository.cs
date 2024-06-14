using Demo.Data;
using Demo.DTOs.Cart;
using Demo.Interfaces;
using Demo.Mappers;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace Demo.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DemoDbContext _context;

        public CartRepository(DemoDbContext context)
        {
            _context = context;

        }

        public async Task<string> AddToCartAsync(Guid cartId, Guid productId, int buyQuanlity = 1)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null)
                throw new Exception("Please sign in first!");
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception("Product does not exist!");
            if (!(buyQuanlity > 0 && buyQuanlity <= product.ProductQuantity))
                throw new Exception($"Quantity: Minimum is 1 and Maximum is {product.ProductQuantity}");
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId);
            
            if (cartItem == null)
            {
                var newCartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = productId,
                    BuyQuanlity = buyQuanlity,
                    Total = product.ProductPrice * buyQuanlity
                };
                await _context.CartItems.AddAsync(newCartItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                cartItem.BuyQuanlity += buyQuanlity;
                cartItem.Total = product.ProductPrice* cartItem.BuyQuanlity;
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
            
            return $"Đã thêm {product.ProductName} vào giỏ hàng!";
        }

        public async Task CreateCartAsync(AppUser user)
        {
            var cart = new Cart
            {
                UserId = user.Id,
            };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItemDTO>> GetAllCartItemAsync(string cartId)
        {
            var cart = await _context.Carts.FindAsync(Guid.Parse(cartId));
            var products = await _context.Products.ToListAsync();
            if (cart == null)
                throw new Exception("Please sign in first!");
            var cartItems = await _context.CartItems.Where(c => c.CartId == Guid.Parse(cartId)).ToListAsync();
            var cartItemDTOs = new List<CartItemDTO>();
            foreach (var cartItem in cartItems)
                foreach (var product in products)
                {
                    if (cartItem.ProductId == product.ProductId)
                    {
                        var cartItemDTO = new CartItemDTO
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductPrice = product.ProductPrice,
                            BuyQuanlity = cartItem.BuyQuanlity,
                            Total = cartItem.Total,
                        };
                        cartItemDTOs.Add(cartItemDTO);
                    }
                }
            return cartItemDTOs;
        }

        public async Task<string> GetCartIdByUserAsync(AppUser user)
        {
            var cartByUser = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == user.Id);
            if (cartByUser == null)
                throw new Exception("User invalid!");
            return cartByUser.CartId.ToString();
        }
        public async Task<string> RemoveFromCartAsync(Guid cartId, Guid productId)
        {
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId);
            if (cartItem == null)
                throw new Exception("Item not found!");
            var product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == productId);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return $"Đã xóa {product.ProductName} khỏi giỏ hàng!";

        }
    }
}
