using E_com_Web.Models;
using E_com_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_com_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ProductsController : Controller
    {
        private readonly IShoeService _shoeService;

        public ProductsController(IShoeService shoeService)
        {
            _shoeService = shoeService;
        }

        // GET: /Admin/Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _shoeService.GetAllShoesAsync();
            return View(items);
        }

        // GET: /Admin/Products/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _shoeService.GetCategoriesAsync();
            return View();
        }

        // POST: /Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [FromForm] string name, 
            [FromForm] string brand,
            [FromForm] string? description, 
            [FromForm] decimal price, 
            [FromForm] string category, 
            [FromForm] int stock,
            [FromForm] string? imageUrl,
            [FromForm] IFormFile? imageFile,
            [FromForm] string? sizes,
            [FromForm] string? colors)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Product name is required.";
                ViewBag.Categories = await _shoeService.GetCategoriesAsync();
                return View();
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                TempData["Error"] = "Brand is required.";
                ViewBag.Categories = await _shoeService.GetCategoriesAsync();
                return View();
            }

            if (price <= 0)
            {
                TempData["Error"] = "Price must be greater than zero.";
                ViewBag.Categories = await _shoeService.GetCategoriesAsync();
                return View();
            }

            // Handle image upload
            string finalImageUrl = imageUrl ?? "https://via.placeholder.com/300";
            
            if (imageFile != null && imageFile.Length > 0)
            {
                try
                {
                    // Create uploads directory if it doesn't exist
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                    Directory.CreateDirectory(uploadsFolder);

                    // Generate unique filename
                    var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Set image URL to relative path
                    finalImageUrl = $"/images/products/{uniqueFileName}";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Image upload failed: {ex.Message}";
                    ViewBag.Categories = await _shoeService.GetCategoriesAsync();
                    return View();
                }
            }

            var shoe = new Shoe
            {
                Name = name,
                Brand = brand,
                Description = description ?? string.Empty,
                Price = price,
                Category = category,
                Stock = stock,
                ImageUrl = finalImageUrl,
                Sizes = ParseCommaSeparated(sizes),
                Colors = ParseCommaSeparated(colors),
                Rating = 4.5m,
                ReviewCount = 0,
                ShowOnHome = true  // Default to show on home
            };

            await _shoeService.AddShoeAsync(shoe);
            TempData["Success"] = $"Product '{name}' created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _shoeService.GetShoeByIdAsync(id);
            if (item == null) return NotFound();
            ViewBag.Categories = await _shoeService.GetCategoriesAsync();
            ViewBag.SizesString = string.Join(",", item.Sizes);
            ViewBag.ColorsString = string.Join(",", item.Colors);
            return View(item);
        }

        // POST: /Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [FromForm] string name, 
            [FromForm] string brand,
            [FromForm] string? description, 
            [FromForm] decimal price, 
            [FromForm] string category, 
            [FromForm] int stock,
            [FromForm] string? imageUrl,
            [FromForm] string? sizes,
            [FromForm] string? colors,
            [FromForm] decimal? rating,
            [FromForm] int? reviewCount)
        {
            var existingShoe = await _shoeService.GetShoeByIdAsync(id);
            if (existingShoe == null) return NotFound();

            existingShoe.Name = name;
            existingShoe.Brand = brand;
            existingShoe.Description = description ?? string.Empty;
            existingShoe.Price = price;
            existingShoe.Category = category;
            existingShoe.Stock = stock;
            existingShoe.ImageUrl = imageUrl ?? existingShoe.ImageUrl;
            existingShoe.Sizes = ParseCommaSeparated(sizes);
            existingShoe.Colors = ParseCommaSeparated(colors);
            existingShoe.Rating = rating;
            existingShoe.ReviewCount = reviewCount;

            await _shoeService.UpdateShoeAsync(existingShoe);
            TempData["Success"] = $"Product '{name}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Products/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _shoeService.GetShoeByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /Admin/Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _shoeService.GetShoeByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] int id)
        {
            var shoe = await _shoeService.GetShoeByIdAsync(id);
            if (shoe == null) return NotFound();

            await _shoeService.DeleteShoeAsync(id);
            TempData["Success"] = $"Product '{shoe.Name}' deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Products/UpdatePrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePrice([FromForm] int id, [FromForm] decimal price)
        {
            var shoe = await _shoeService.GetShoeByIdAsync(id);
            if (shoe == null) return NotFound();

            shoe.Price = price;
            await _shoeService.UpdateShoeAsync(shoe);
            
            TempData["Success"] = $"Product '{shoe.Name}' price updated to {price:C}.";
            return RedirectToAction(nameof(Index));
        }

        // Helper method to parse comma-separated strings
        private List<string> ParseCommaSeparated(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<string>();

            return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();
        }
    }
}
