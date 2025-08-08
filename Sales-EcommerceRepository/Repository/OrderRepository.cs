using Microsoft.EntityFrameworkCore;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.Context;
using Sales_EcommerceRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        readonly Sales_EcommerceDbContext _EcommerceDbContext;
        public OrderRepository(Sales_EcommerceDbContext EcommerceDbContext)
        {
            _EcommerceDbContext = EcommerceDbContext;
        }

        public async Task<bool> AddOrderItemAsync(List<OrderItem> orderItem)
        {
            try
            {
                foreach (var item in orderItem)
                {
                    await _EcommerceDbContext.OrderItem.AddAsync(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await Task.Run(() =>
            {
                var order = _EcommerceDbContext.Order.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    _EcommerceDbContext.Order.Remove(order);
                    _EcommerceDbContext.SaveChanges();
                    return true;
                }
                return false;
            });
        }

        public async Task<bool> DeleteOrderItemAsync(int orderId)
        {
            var orderItem = await _EcommerceDbContext.OrderItem.FirstOrDefaultAsync(o => o.OrderItemId == orderId);
            if (orderItem != null)
            {
                _EcommerceDbContext.OrderItem.Remove(orderItem);
                await _EcommerceDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await Task.Run(() =>
            {
                return _EcommerceDbContext.Order.ToList();
            });
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await Task.Run(() =>
            {
                return _EcommerceDbContext.Order.FirstOrDefault(o => o.OrderId == orderId);
            });
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(int productId)
        {
            return await _EcommerceDbContext.OrderItem.Where(o => o.ProductId == productId).ToListAsync();
        }


        public async Task<OrderItem> GetOrderItemsByUserIdAsync(int id)
        {
            return await _EcommerceDbContext.OrderItem.FirstOrDefaultAsync(o => o.OrderItemId == id);
        }

        public async Task<bool> PlaceOrderAsync(Order order)
        {
            return await Task.Run(() =>
            {
                if (order != null)
                {
                    _EcommerceDbContext.Order.Add(order);
                    _EcommerceDbContext.SaveChanges();
                    return true;
                }
                return false;
            });
        }

        public async Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
        {
            return await _EcommerceDbContext.OrderItem
              .Where(p => p.OrderItemId == orderItem.OrderItemId)
              .ExecuteUpdateAsync(upd => upd
                  .SetProperty(p => p.OrderId, orderItem.OrderId)
                  .SetProperty(p => p.ProductId, orderItem.ProductId)
                  .SetProperty(p => p.Quantity, orderItem.Quantity))
              .ContinueWith(t => t.Result > 0);
        }
    }
}
