using Demo.Models;
using Demo.DTOs.Order;

namespace Demo.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO ToOrderDTO(this Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                OrderCode = order.OrderCode,
                ReceivedEmail = order.ReceivedEmail,
                ReceivedName = order.ReceivedName,
                ReceivedPhoneNumber = order.ReceivedPhoneNumber,
                ReceivedDate = order.ReceivedDate,
                ReceivedAddress = order.ReceivedAddress,
                OrderStatus = order.OrderStatus,
                CreatedDate = order.CreatedDate,
                ModifiedDate = order.ModifiedDate,
                OrderDetails = order.OrderDetails.Select(s=>s.ToOrderDetailDTO()).ToList()
            };
        }
        public static Order ToCreateOrderDTO(this CreateOrderDTO order)
        {
            return new Order
            {
                ReceivedEmail = order.ReceivedEmail,
                ReceivedAddress = order.ReceivedAddress,
                ReceivedName = order.ReceivedName,
                ReceivedPhoneNumber = order.ReceivedPhoneNumber,
            };
        }
    }
}
