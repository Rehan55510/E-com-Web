using E_com_Web.Models;
using E_com_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_com_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /Admin/Orders
        [HttpGet]
        public async Task<IActionResult> Index(string? status)
        {
            var orders = await _orderService.GetAllOrdersAsync();
            
            // Filter by status if provided
            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.OrderStatus == status);
            }

            ViewBag.SelectedStatus = status;
            return View(orders);
        }

        // GET: /Admin/Orders/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: /Admin/Orders/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            await _orderService.UpdateOrderStatusAsync(id, status);
            TempData["Success"] = $"Order #{id} status updated to {status}.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
