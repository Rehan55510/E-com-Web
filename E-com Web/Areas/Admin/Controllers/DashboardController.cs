using E_com_Web.Areas.Admin.Models;
using E_com_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_com_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class DashboardController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoeService _shoeService;

        public DashboardController(IOrderService orderService, IShoeService shoeService)
        {
            _orderService = orderService;
            _shoeService = shoeService;
        }

        // GET: /Admin/Dashboard
        public async Task<IActionResult> Index()
        {
            var stats = new DashboardStats
            {
                TotalOrders = await _orderService.GetTotalOrdersCountAsync(),
                TotalRevenue = await _orderService.GetTotalRevenueAsync(),
                OrdersByStatus = await _orderService.GetOrdersByStatusAsync(),
                RecentOrders = await _orderService.GetRecentOrdersAsync(10)
            };

            // Get product stats
            var allProducts = await _shoeService.GetAllShoesAsync();
            stats.TotalProducts = allProducts.Count();
            stats.LowStockProducts = allProducts.Count(p => p.Stock < 10);

            return View(stats);
        }
    }
}
