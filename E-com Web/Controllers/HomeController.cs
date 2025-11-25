using Microsoft.AspNetCore.Mvc;
using E_com_Web.Services;

namespace E_com_Web.Controllers;

public class HomeController : Controller
{
    private readonly IShoeService _shoeService;

    public HomeController(IShoeService shoeService)
    {
        _shoeService = shoeService;
    }

    public async Task<IActionResult> Index()
    {
        var shoes = await _shoeService.GetAllShoesAsync();
        var featuredShoes = shoes.Take(6).ToList();
        return View(featuredShoes);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}

