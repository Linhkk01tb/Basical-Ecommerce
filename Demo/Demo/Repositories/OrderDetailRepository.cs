using Demo.Data;
using Demo.DTOs.Order;
using Demo.DTOs.OrderDetailDTO;
using Demo.Enums;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DemoDbContext _context;

        public OrderDetailRepository(DemoDbContext context)
        {
            _context = context;
        }
        public async Task CreateOrderDetailAsync(Guid orderId, Guid cartId)
        {
            var carts = await _context.CartItems.Where(c => c.CartId == cartId).ToListAsync();
            if (carts.Count() == 0)
                throw new Exception("Your cart is empty!");
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new Exception("Order does not exist!");
            var orders = await _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
            foreach (var od in orders)
                foreach (var ct in carts)
                {
                    if (od.ProductId == ct.ProductId)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = orderId,
                            ProductId = ct.ProductId,
                            BuyQuantity = ct.BuyQuanlity,
                            Total = ct.Total,
                        };
                        await _context.OrderDetails.AddAsync(orderDetail);
                    }
                }
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
                foreach (var od in orders)
                {
                    if (od.ProductId == product.ProductId)
                        product.ProductQuantity -= od.BuyQuantity;
                    _context.Products.Update(product);
                }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(Guid orderId)
        {
            var orderDetail = await _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
            _context.OrderDetails.RemoveRange(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetailDTO>> GetByOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new Exception("Order not found!");
            var orderDetails = await _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
            var products = await _context.Products.ToListAsync();
            var orderDetailDTOs = new List<OrderDetailDTO>();
            foreach (var product in products)
                foreach (var od in orderDetails)
                {
                    if (od.ProductId == product.ProductId)
                    {
                        var item = new OrderDetailDTO
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductPrice = product.ProductPrice,
                            BuyQuantity = od.BuyQuantity,
                            Total = od.Total,
                        };
                        orderDetailDTOs.Add(item);
                    }
                }
            return orderDetailDTOs;
        }
    }
}
