using E_com_Web.Models;
using E_com_Web.Services;
using E_com_Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_com_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/admin")]
public class AdminApiController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IShoeService _shoeService;

    public AdminApiController(IOrderService orderService, IShoeService shoeService)
    {
        _orderService = orderService;
        _shoeService = shoeService;
    }

    /// <summary>
    /// GET /api/admin/orders?page=1&pageSize=10
    /// Returns paginated list of all real orders
    /// </summary>
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var allOrders = await _orderService.GetAllOrdersAsync();
        var totalOrders = allOrders.Count();
        var totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

        var paginatedOrders = allOrders
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new
            {
                o.Id,
                o.OrderDate,
                o.TotalAmount,
                o.OrderStatus,
                o.PaymentStatus,
                CustomerName = o.Customer.FullName,
                CustomerEmail = o.Customer.Email,
                CustomerPhone = o.Customer.Phone,
                ItemCount = o.OrderItems.Count
            })
            .ToList();

        return Ok(new
        {
            page,
            pageSize,
            totalOrders,
            totalPages,
            orders = paginatedOrders
        });
    }

    /// <summary>
    /// GET /api/admin/orders/{id}
    /// Shows full order details + user address + phone
    /// </summary>
    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound(new { message = $"Order #{id} not found" });
        }

        return Ok(new
        {
            order.Id,
            order.OrderDate,
            order.TotalAmount,
            order.OrderStatus,
            order.PaymentStatus,
            order.PaymentMethod,
            customer = new
            {
                name = order.Customer.FullName,
                email = order.Customer.Email,
                phone = order.Customer.Phone,
                address = order.Customer.FullAddress
            },
            items = order.OrderItems.Select(item => new
            {
                item.Id,
                item.ProductName,
                item.Brand,
                item.Size,
                item.Color,
                item.Quantity,
                item.PricePerUnit,
                item.Subtotal
            })
        });
    }

    /// <summary>
    /// PUT /api/admin/orders/{id}/status
    /// Updates order status
    /// </summary>
    [HttpPut("orders/{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateStatusRequest request)
    {
        if (string.IsNullOrEmpty(request.Status))
        {
            return BadRequest(new { message = "Status is required" });
        }

        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound(new { message = $"Order #{id} not found" });
        }

        await _orderService.UpdateOrderStatusAsync(id, request.Status);

        return Ok(new
        {
            message = $"Order #{id} status updated to {request.Status}",
            orderId = id,
            newStatus = request.Status
        });
    }

    /// <summary>
    /// GET /api/admin/products/home
    /// Returns products that appear on Home page
    /// </summary>
    [HttpGet("products/home")]
    public async Task<IActionResult> GetHomeProducts()
    {
        var allProducts = await _shoeService.GetAllShoesAsync();
        var homeProducts = allProducts
            .Where(p => p.ShowOnHome)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Brand,
                p.Price,
                p.Category,
                p.Stock,
                p.ImageUrl,
                p.Rating,
                p.ShowOnHome
            })
            .ToList();

        return Ok(new
        {
            count = homeProducts.Count,
            products = homeProducts
        });
    }

    /// <summary>
    /// GET /api/admin/analytics
    /// Returns real-time analytics based on real DB data
    /// </summary>
    [HttpGet("analytics")]
    public async Task<IActionResult> GetAnalytics()
    {
        var totalOrders = await _orderService.GetTotalOrdersCountAsync();
        var totalRevenue = await _orderService.GetTotalRevenueAsync();
        var ordersByStatus = await _orderService.GetOrdersByStatusAsync();
        var recentOrders = await _orderService.GetRecentOrdersAsync(10);

        var allProducts = await _shoeService.GetAllShoesAsync();
        var totalProducts = allProducts.Count();
        var lowStockProducts = allProducts.Count(p => p.Stock < 10);

        return Ok(new
        {
            totalOrders,
            totalRevenue,
            totalProducts,
            lowStockProducts,
            ordersByStatus,
            recentOrders = recentOrders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                o.TotalAmount,
                o.OrderStatus,
                customerName = o.Customer.FullName
            }),
            timestamp = DateTime.Now
        });
    }
}

public class UpdateStatusRequest
{
    public string Status { get; set; } = string.Empty;
}
