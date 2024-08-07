﻿using Demo.Data;
using Demo.DTOs.Order;
using Demo.Enums;
using Demo.Helpers.QueryObjects;
using Demo.Interfaces;
using Demo.Models;
using Microsoft.EntityFrameworkCore;


namespace Demo.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DemoDbContext _context;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(DemoDbContext context, IOrderDetailRepository orderDetailRepository, ICartRepository cartRepository)
        {
            _context = context;
            _orderDetailRepository = orderDetailRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Order> CreateOrderAsync(AppUser user, CreateOrderDTO orderDTO)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var newOrder = new Order
            {
                UserId = user.Id,
                ReceivedName = orderDTO.ReceivedName,
                ReceivedEmail = orderDTO.ReceivedEmail,
                ReceivedPhoneNumber = orderDTO.ReceivedPhoneNumber,
                ReceivedAddress = orderDTO.ReceivedAddress,
                OrderCode = "OD" + new Random().Next(999) + new Random().Next(999) + new Random().Next(999) + new Random().Next(999),
                ReceivedDate = DateTime.UtcNow.ToLocalTime().AddDays(4),
                CreatedDate = DateTime.UtcNow.ToLocalTime(),
                ModifiedDate = DateTime.UtcNow.ToLocalTime()
            };
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order?> DeleteOrderAsync(Guid orderId)
        {
            var deleteOrder = await _context.Orders!.Include(od => od.OrderDetails).ThenInclude(od => od.Product).SingleOrDefaultAsync(od => od.OrderId == orderId);
            if (deleteOrder == null)
                return null;
            _context.Orders.Remove(deleteOrder);
            await _orderDetailRepository.DeleteOrderDetailAsync(orderId);
            await _context.SaveChangesAsync();
            return deleteOrder;
        }

        public async Task<List<Order>> GetAllOrdersAsync(OrderQueryObject queryObject)
        {
            var orders = _context.Orders!.Include(od => od.OrderDetails).ThenInclude(od => od.Product).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.OrderCode))
            {
                orders = orders.Where(ct => ct.OrderCode.Contains(queryObject.OrderCode));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy)
                {
                    case "CreatedDate":
                        orders = queryObject.IsDescending ? orders.OrderByDescending(ct => ct.CreatedDate) : orders.OrderBy(ct => ct.CreatedDate);
                        break;
                    case "ModifiedDate":
                        orders = queryObject.IsDescending ? orders.OrderByDescending(ct => ct.ModifiedDate) : orders.OrderBy(ct => ct.ModifiedDate);
                        break;
                    default:
                        orders = orders.OrderByDescending(ct => ct.CreatedDate);
                        break;
                }
            }

            return await orders.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            var orderById = await _context.Orders!.Include(od => od.OrderDetails).ThenInclude(od=>od.Product).SingleOrDefaultAsync(od => od.OrderId == orderId);
            if (orderById == null)
                return null;
            return orderById;
        }

        public async Task<List<Order>> GetOrderByUserAsync(AppUser user, OrderQueryObject queryObject)
        {
            var orders = _context.Orders!.Where(u => u.UserId == user.Id).Include(od=>od.OrderDetails).ThenInclude(od=>od.Product).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.OrderCode))
            {
                orders = orders.Where(ct => ct.OrderCode.Contains(queryObject.OrderCode));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy)
                {
                    case "OrderDate":
                        orders = queryObject.IsDescending ? orders.OrderByDescending(ct => ct.CreatedDate) : orders.OrderBy(ct => ct.CreatedDate);
                        break;
                    case "ModifiedDate":
                        orders = queryObject.IsDescending ? orders.OrderByDescending(ct => ct.ModifiedDate) : orders.OrderBy(ct => ct.ModifiedDate);
                        break;
                    default:
                        orders = orders.OrderByDescending(ct => ct.CreatedDate);
                        break;
                }
            }

            return await orders.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToListAsync();
        }
        public async Task<Order?> UpdateOrderAsync(Guid orderId, OrderStatusCodes orderStatusCodes)
        {
            var updateOrder = await _context.Orders!.SingleOrDefaultAsync(od => od.OrderId == orderId);
            if (updateOrder == null)
                return null;
            if (orderStatusCodes == OrderStatusCodes.Completed)
            {
                updateOrder.OrderStatus = orderStatusCodes;
                updateOrder.ReceivedDate = DateTime.UtcNow.ToLocalTime();
                updateOrder.ModifiedDate = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                updateOrder.OrderStatus = orderStatusCodes;
                updateOrder.ModifiedDate = DateTime.UtcNow.ToLocalTime();
            }
            _context.Orders.Update(updateOrder);
            await _context.SaveChangesAsync();
            return updateOrder;
        }
    }
}
