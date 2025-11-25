using E_com_Web.Data.Repositories;
using E_com_Web.Models;
using Microsoft.EntityFrameworkCore;
using E_com_Web.Data;

namespace E_com_Web.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Shoe> _shoeRepository;
    private readonly ApplicationDbContext _context;

    public OrderService(IRepository<Order> orderRepository, IRepository<Shoe> shoeRepository, ApplicationDbContext context)
    {
        _orderRepository = orderRepository;
        _shoeRepository = shoeRepository;
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(CheckoutViewModel checkout, Dictionary<int, CartItem> cart)
    {
        var order = new Order
        {
            OrderDate = DateTime.Now,
            OrderStatus = "Pending",
            PaymentStatus = "Pending",
            PaymentMethod = "CreditCard",
            TotalAmount = cart.Values.Sum(item => item.Total)
        };

        // Create customer details
        order.Customer = new OrderCustomer
        {
            FirstName = checkout.FirstName,
            LastName = checkout.LastName,
            Email = checkout.Email,
            Phone = checkout.Phone,
            Address = checkout.Address,
            City = checkout.City,
            State = checkout.State,
            ZipCode = checkout.ZipCode,
            Country = checkout.Country
        };

        // Create order items
        foreach (var item in cart.Values)
        {
            var shoe = await _shoeRepository.GetByIdAsync(item.ShoeId);
            var brand = shoe?.Brand ?? "Unknown";
            
            var orderItem = new OrderItem
            {
                ShoeId = item.ShoeId,
                ProductName = item.Name,
                Brand = brand,
                Size = item.Size,
                Color = item.Color,
                Quantity = item.Quantity,
                PricePerUnit = item.Price,
                Subtotal = item.Total
            };
            order.OrderItems.Add(orderItem);
        }

        await _orderRepository.AddAsync(order);
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order != null)
        {
            order.OrderStatus = status;
            await _orderRepository.UpdateAsync(order);
        }
    }

    public async Task<int> GetTotalOrdersCountAsync()
    {
        return await _context.Orders.CountAsync();
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.Orders
            .Where(o => o.PaymentStatus == "Paid" || o.OrderStatus == "Completed")
            .SumAsync(o => o.TotalAmount);
    }

    public async Task<Dictionary<string, int>> GetOrdersByStatusAsync()
    {
        return await _context.Orders
            .GroupBy(o => o.OrderStatus)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Status, x => x.Count);
    }

    public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int count = 10)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .ToListAsync();
    }
}
