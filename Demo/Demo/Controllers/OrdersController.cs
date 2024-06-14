using Demo.DTOs.Order;
using Demo.Enums;
using Demo.Helpers;
using Demo.Helpers.Extensions;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Mappers;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("all_order")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> GetAll([FromQuery] OrderQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var orders = await _orderRepository.GetAllOrdersAsync(queryObject);
            var ordersRes = orders.Select(s => s.ToOrderDTO()).ToList();
            return StatusCode(StatusCodes.Status200OK, new
            {
                pageNumber = queryObject.PageNumber,
                pageSize = queryObject.PageSize,
                ordersRes
            });
        }

        [HttpGet]
        [Route("user-order")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> GetByUser([FromQuery] OrderQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
                return StatusCode(StatusCodes.Status403Forbidden);
            var orders = await _orderRepository.GetOrderByUserAsync(appUser, queryObject);
            var ordersRes = orders.Select(s => s.ToOrderDTO()).ToList();
            return StatusCode(StatusCodes.Status200OK, new
            {
                pageNumber = queryObject.PageNumber,
                pageSize = queryObject.PageSize,
                ordersRes
            });
        }
        [HttpGet("{orderId:guid}")]
        [Authorize]

        public async Task<IActionResult> GetById(Guid orderId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var orderById = await _orderRepository.GetOrderByIdAsync(orderId);
                if (orderById == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                return StatusCode(StatusCodes.Status200OK, orderById.ToOrderDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Create(CreateOrderDTO orderDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var userName = User.GetUserName();
                var appUser = await _userManager.FindByNameAsync(userName);
                if (appUser == null)
                    return StatusCode(StatusCodes.Status401Unauthorized);
                var newOrder = await _orderRepository.CreateOrderAsync(appUser, orderDTO);
                var order = newOrder.ToOrderDTO();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    IsCreated = true,
                    order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpPut("Payment{orderId:guid}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> UpdatePaymentStatus(Guid orderId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var updateOrder = await _orderRepository.UpdateOrderAsync(orderId, OrderStatusCodes.Payment);
                if (updateOrder == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                var order = updateOrder.ToOrderDTO();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    Payment = true,
                    order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPut("Completed{orderId:guid}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> UpdateCompletedStatus(Guid orderId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var updateOrder = await _orderRepository.UpdateOrderAsync(orderId, OrderStatusCodes.Completed);
                if (updateOrder == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                var order = updateOrder.ToOrderDTO();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    Completed = true,
                    order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }
        [HttpPut("Cancel{orderId:guid}")]
        [Authorize(Roles = AppRoles.Admin + "," + AppRoles.User)]
        public async Task<IActionResult> UpdateCancelledStatus(Guid orderId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var orderById = await _orderRepository.GetOrderByIdAsync(orderId);
                if (orderById == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                if (!(orderById.OrderStatus == OrderStatusCodes.New))
                    return StatusCode(StatusCodes.Status403Forbidden, "Can't cancel this order!");

                var updateOrder = await _orderRepository.UpdateOrderAsync(orderId, OrderStatusCodes.Cancelled);
                var order = updateOrder.ToOrderDTO();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    Cancelled = true,
                    order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpDelete("{orderId:guid}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                var deleteOrder = await _orderRepository.DeleteOrderAsync(orderId);
                if (deleteOrder == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                var order = deleteOrder.ToOrderDTO();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    IsDeleted = true,
                    order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }
    }
}
