using Microsoft.AspNetCore.Mvc;
using E_com_Web.Models;
using E_com_Web.Services;
using System.Text.Json;

namespace E_com_Web.Controllers;

public class CartController : Controller
{
    private readonly IShoeService _shoeService;
    private readonly ICartService _cartService;
    private const string CartSessionKey = "Cart";

    public CartController(IShoeService shoeService, ICartService cartService)
    {
        _shoeService = shoeService;
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

    private void SaveCart(Dictionary<int, CartItem> cart)
    {
        var cartJson = JsonSerializer.Serialize(cart, JsonOptions);
        HttpContext.Session.SetString(CartSessionKey, cartJson);
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        var cartViewModel = _cartService.GetCart(cart);
        return View(cartViewModel);
    }

    [HttpPost]
    public IActionResult AddToCart(int shoeId, string size, string color, int quantity = 1)
    {
        var shoe = _shoeService.GetShoeById(shoeId);
        if (shoe == null)
        {
            return NotFound();
        }

        var cart = GetCart();
        _cartService.AddToCart(cart, shoe, size, color, quantity);
        SaveCart(cart);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int shoeId)
    {
        var cart = GetCart();
        _cartService.RemoveFromCart(cart, shoeId);
        SaveCart(cart);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int shoeId, int quantity)
    {
        var cart = GetCart();
        _cartService.UpdateQuantity(cart, shoeId, quantity);
        SaveCart(cart);

        return RedirectToAction("Index");
    }

    public IActionResult GetCartCount()
    {
        var cart = GetCart();
        var count = cart.Values.Sum(item => item.Quantity);
        return Json(new { count });
    }
}

