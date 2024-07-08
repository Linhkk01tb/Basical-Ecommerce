using Demo.DTOs;
using Demo.Models;

namespace Demo.Mappers
{
    public static class OrderDetailMapper
    {
        public static OrderDetailDTO ToOrderDetailDTO(this OrderDetail orderDetail)
        {
            return new OrderDetailDTO
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                ProductName = orderDetail.Product.ProductName,
                ProductPrice = orderDetail.Product.ProductPrice,
                BuyQuantity = orderDetail.BuyQuantity,
                Total = orderDetail.Total,
            };
        }
    }
}
