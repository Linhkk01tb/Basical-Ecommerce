using Demo.Helpers.Extensions;
using Demo.Interfaces;
using Demo.Models;
using Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartController(UserManager<AppUser> userManager, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cartId = User.GetCartId();
                if (cartId == null)
                    return StatusCode(StatusCodes.Status403Forbidden);
                var cartItems = await _cartRepository.GetAllCartItemAsync(cartId);
                return StatusCode(StatusCodes.Status200OK, cartItems);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int buyQuantity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var cartId = User.GetCartId();
                var cartItem = await _cartRepository.AddToCartAsync(Guid.Parse(cartId), productId, buyQuantity);
                return StatusCode(StatusCodes.Status200OK, cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var cartId = User.GetCartId();
                var product = await _productRepository.GetProductsByIdAsync(productId);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                var removeCartItem = await _cartRepository.RemoveFromCartAsync(Guid.Parse(cartId), productId);
                return StatusCode(StatusCodes.Status200OK, removeCartItem);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

    }
}
