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

    public async Task<IActionResult> Index(string? category, string? search)
    {
        var shoes = await _shoeService.GetAllShoesAsync();
        
        if (!string.IsNullOrEmpty(category))
        {
            shoes = await _shoeService.GetShoesByCategoryAsync(category);
        }
        
        if (!string.IsNullOrEmpty(search))
        {
            shoes = await _shoeService.SearchShoesAsync(search);
        }

        ViewBag.Categories = await _shoeService.GetCategoriesAsync();
        ViewBag.SelectedCategory = category;
        ViewBag.SearchTerm = search;
        
        return View(shoes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var shoe = await _shoeService.GetShoeByIdAsync(id);
        if (shoe == null)
        {
            return NotFound();
        }
        return View(shoe);
    }
}

