using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            var items = _shoeService.GetAllShoes();
            return View(items);
        }

        // GET: /Admin/Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _shoeService.GetCategories();
            return View();
        }

        // POST: /Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] string name, [FromForm] string? description, [FromForm] decimal price, [FromForm] string category, [FromForm] int quantity, IFormFile? image)
        {
            // TODO: save to DB (EF) and handle image upload
            TempData["Success"] = $"Product '{name}' created at {price:C}.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Products/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _shoeService.GetShoeById(id);
            if (item == null) return NotFound();
            ViewBag.Categories = _shoeService.GetCategories();
            return View(item);
        }

        // POST: /Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [FromForm] string name, [FromForm] string? description, [FromForm] decimal price, [FromForm] string category, [FromForm] int quantity, IFormFile? image)
        {
            // TODO: update in DB (EF)
            TempData["Success"] = $"Product #{id} updated.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Products/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = _shoeService.GetShoeById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /Admin/Products/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _shoeService.GetShoeById(id);
            if (item == null) return NotFound();
            return View(item);
        }

// POST: /Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            // TODO: delete via EF
            TempData["Success"] = $"Product #{id} deleted.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Products/UpdatePrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePrice([FromForm] int id, [FromForm] decimal price)
        {
            TempData["Success"] = $"Product #{id} price update queued to {price:C}.";
            return RedirectToAction(nameof(Index));
        }
    }
}
