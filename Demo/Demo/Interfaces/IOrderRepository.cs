using Demo.DTOs;
using Demo.DTOs.Order;
using Demo.Enums;
using Demo.Helpers.QueryObjects;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync(OrderQueryObject queryObject);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<Order> CreateOrderAsync(AppUser user,CreateOrderDTO orderDTO);
        Task<Order?> UpdateOrderAsync(Guid orderId, OrderStatusCodes orderStatusCodes);
        Task<Order?> DeleteOrderAsync(Guid orderId);
        Task<List<Order>> GetOrderByUserAsync(AppUser user, OrderQueryObject queryObject);
    }
}
