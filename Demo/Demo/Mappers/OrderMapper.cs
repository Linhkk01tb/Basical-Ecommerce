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
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                CreatedDate = order.CreatedDate,
                ModifiedDate = order.ModifiedDate
            };
        }
        public static Order ToCreateOrderDTO(this CreateOrderDTO order)
        {
            return new Order
            {
                ReceivedEmail = order.ReceivedEmail,
                ReceivedName = order.ReceivedName,
                ReceivedPhoneNumber = order.ReceivedPhoneNumber,
            };
        }
    }
}
