using Microsoft.AspNetCore.Mvc;
using E_com_Web.Models;
using E_com_Web.Services;
using System.Text.Json;

namespace E_com_Web.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private const string CartSessionKey = "Cart";

    public CheckoutController(ICartService cartService)
    {
        _cartService = cartService;
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
    public IActionResult ProcessOrder(CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var cart = GetCart();
            model.Cart = _cartService.GetCart(cart);
            return View("Index", model);
        }

        // Clear cart
        HttpContext.Session.Remove(CartSessionKey);

        // In a real application, you would process the payment and save the order here
        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }
}

