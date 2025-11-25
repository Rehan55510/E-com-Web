using Microsoft.AspNetCore.Mvc;
using E_com_Web.Models;
using E_com_Web.Services;
using E_com_Web.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace E_com_Web.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IHubContext<OrderHub> _hubContext;
    private const string CartSessionKey = "Cart";

    public CheckoutController(ICartService cartService, IOrderService orderService, IHubContext<OrderHub> hubContext)
    {
        _cartService = cartService;
        _orderService = orderService;
        _hubContext = hubContext;
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false
    };

    private Dictionary<int, CartItem> GetCart()
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(cartJson))
        {
            return new Dictionary<int, CartItem>();
        }
        try
        {
            return JsonSerializer.Deserialize<Dictionary<int, CartItem>>(cartJson, JsonOptions) ?? new Dictionary<int, CartItem>();
        }
        catch
        {
            return new Dictionary<int, CartItem>();
        }
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        if (cart.Count == 0)
        {
            return RedirectToAction("Index", "Cart");
        }

        var cartViewModel = _cartService.GetCart(cart);
        var checkoutViewModel = new CheckoutViewModel
        {
            Cart = cartViewModel
        };

        return View(checkoutViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessOrder(CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var cart = GetCart();
            model.Cart = _cartService.GetCart(cart);
            return View("Index", model);
        }

        var cartItems = GetCart();
        
        // Create real order in database
        var order = await _orderService.CreateOrderAsync(model, cartItems);

        // Clear cart after successful order
        HttpContext.Session.Remove(CartSessionKey);

        // Broadcast to all admin users via SignalR
        await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNewOrder", 
            order.Id, 
            order.Customer.FullName, 
            order.TotalAmount);

        // Redirect to success with order ID
        return RedirectToAction("Success", new { orderId = order.Id });
    }

    public IActionResult Success(int orderId)
    {
        ViewBag.OrderId = orderId;
        return View();
    }
}

