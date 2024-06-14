using Demo.DTOs.OrderDetailDTO;

namespace Demo.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetailDTO>> GetByOrderAsync(Guid orderId);
        Task CreateOrderDetailAsync(Guid orderId, Guid cartId);
        Task DeleteOrderDetailAsync(Guid orderId);
    }
}
