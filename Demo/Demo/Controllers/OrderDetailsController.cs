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
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailsController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetAll(Guid orderId)
        {
            try
            {
                var cartId = User.GetCartId();
                if (cartId == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please sign in first!");
                var orderDetails = await _orderDetailRepository.GetByOrderAsync(orderId);
                return StatusCode(StatusCodes.Status200OK, orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("{orderId:guid}")]
        public async Task<IActionResult> Create(Guid orderId)
        {
            try
            {
                var cartId = User.GetCartId();
                await _orderDetailRepository.CreateOrderDetailAsync(orderId, Guid.Parse(cartId));
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
