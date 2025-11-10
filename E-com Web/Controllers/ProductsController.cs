using Microsoft.AspNetCore.Mvc;
using E_com_Web.Services;

namespace E_com_Web.Controllers;

public class ProductsController : Controller
{
    private readonly IShoeService _shoeService;

    public ProductsController(IShoeService shoeService)
    {
        _shoeService = shoeService;
    }

    public IActionResult Index(string? category, string? search)
    {
        var shoes = _shoeService.GetAllShoes();
        
        if (!string.IsNullOrEmpty(category))
        {
            shoes = _shoeService.GetShoesByCategory(category);
        }
        
        if (!string.IsNullOrEmpty(search))
        {
            shoes = _shoeService.SearchShoes(search);
        }

        ViewBag.Categories = _shoeService.GetCategories();
        ViewBag.SelectedCategory = category;
        ViewBag.SearchTerm = search;
        
        return View(shoes);
    }

    public IActionResult Details(int id)
    {
        var shoe = _shoeService.GetShoeById(id);
        if (shoe == null)
        {
            return NotFound();
        }
        return View(shoe);
    }
}

