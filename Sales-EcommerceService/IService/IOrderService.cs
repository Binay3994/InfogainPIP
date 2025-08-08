using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.IService
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<bool> DeleteOrderAsync(int orderId);

        Task<bool> AddOrderItemAsync(List<OrderItem> orderItem);
        Task<bool> UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> DeleteOrderItemAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(int orderId);
        Task<OrderItem> GetOrderItemsByUserIdAsync(int id);
    }
}
