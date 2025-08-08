using Sales_EcommerceModel.DbModel;
using Sales_EcommerceRepository.IRepository;
using Sales_EcommerceService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceService.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddOrderItemAsync(List<OrderItem> orderItem)
        {
            return await _orderRepository.AddOrderItemAsync(orderItem);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
           return await _orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task<bool> DeleteOrderItemAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderItemAsync(orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByProductIdAsync(int orderId)
        { 
             return await _orderRepository.GetOrderItemsByProductIdAsync(orderId);
        }

        public async Task<OrderItem> GetOrderItemsByUserIdAsync(int id)
        {
            return await _orderRepository.GetOrderItemsByUserIdAsync(id);
        }

        public async Task<bool> PlaceOrderAsync(Order order)
        {
            return await _orderRepository.PlaceOrderAsync(order);
        }

        public Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
