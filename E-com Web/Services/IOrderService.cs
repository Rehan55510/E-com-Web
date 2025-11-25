using E_com_Web.Models;

namespace E_com_Web.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(CheckoutViewModel checkout, Dictionary<int, CartItem> cart);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int id);
    Task UpdateOrderStatusAsync(int orderId, string status);
    Task<int> GetTotalOrdersCountAsync();
    Task<decimal> GetTotalRevenueAsync();
    Task<Dictionary<string, int>> GetOrdersByStatusAsync();
    Task<IEnumerable<Order>> GetRecentOrdersAsync(int count = 10);
}
