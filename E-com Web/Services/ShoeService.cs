using E_com_Web.Models;

namespace E_com_Web.Services;

public class ShoeService : IShoeService
{
    private readonly List<Shoe> _shoes = new()
    {
        new Shoe
        {
            Id = 1,
            Name = "Air Max 90",
            Brand = "Nike",
            Description = "Classic running shoes with air cushioning for maximum comfort.",
            Price = 129.99m,
            ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500",
            Category = "Running",
            Sizes = new List<string> { "7", "8", "9", "10", "11", "12" },
            Colors = new List<string> { "Black", "White", "Red" },
            Stock = 50,
            Rating = 4.5m,
            ReviewCount = 234
        },
        new Shoe
        {
            Id = 2,
            Name = "Ultraboost 22",
            Brand = "Adidas",
            Description = "Premium running shoes with Boost technology for energy return.",
            Price = 179.99m,
            ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=500",
            Category = "Running",
            Sizes = new List<string> { "7", "8", "9", "10", "11" },
            Colors = new List<string> { "White", "Black", "Blue" },
            Stock = 30,
            Rating = 4.7m,
            ReviewCount = 189
        },
        new Shoe
        {
            Id = 3,
            Name = "Chuck Taylor All Star",
            Brand = "Converse",
            Description = "Iconic canvas sneakers perfect for everyday wear.",
            Price = 59.99m,
            ImageUrl = "https://images.unsplash.com/photo-1525966222134-fcfa99b8ae77?w=500",
            Category = "Casual",
            Sizes = new List<string> { "6", "7", "8", "9", "10", "11", "12" },
            Colors = new List<string> { "Black", "White", "Red", "Blue" },
            Stock = 100,
            Rating = 4.3m,
            ReviewCount = 456
        },
        new Shoe
        {
            Id = 4,
            Name = "Classic Leather",
            Brand = "Reebok",
            Description = "Timeless leather sneakers with retro style.",
            Price = 79.99m,
            ImageUrl = "https://images.unsplash.com/photo-1551107696-a4b0c5a0d9a2?w=500",
            Category = "Casual",
            Sizes = new List<string> { "7", "8", "9", "10", "11" },
            Colors = new List<string> { "White", "Navy", "Gray" },
            Stock = 45,
            Rating = 4.4m,
            ReviewCount = 167
        },
        new Shoe
        {
            Id = 5,
            Name = "Air Force 1",
            Brand = "Nike",
            Description = "Legendary basketball-inspired sneakers with timeless design.",
            Price = 99.99m,
            ImageUrl = "https://images.unsplash.com/photo-1595950653106-6c9ebd614d3a?w=500",
            Category = "Casual",
            Sizes = new List<string> { "7", "8", "9", "10", "11", "12" },
            Colors = new List<string> { "White", "Black" },
            Stock = 60,
            Rating = 4.6m,
            ReviewCount = 312
        },
        new Shoe
        {
            Id = 6,
            Name = "Gel-Kayano 29",
            Brand = "ASICS",
            Description = "Advanced stability running shoes with GEL technology.",
            Price = 159.99m,
            ImageUrl = "https://images.unsplash.com/photo-1606107557195-0e29a4b5b4aa?w=500",
            Category = "Running",
            Sizes = new List<string> { "7", "8", "9", "10", "11" },
            Colors = new List<string> { "Black", "Blue", "Gray" },
            Stock = 25,
            Rating = 4.8m,
            ReviewCount = 98
        },
        new Shoe
        {
            Id = 7,
            Name = "Old Skool",
            Brand = "Vans",
            Description = "Classic skate shoes with durable canvas and waffle outsole.",
            Price = 69.99m,
            ImageUrl = "https://images.unsplash.com/photo-1539185441755-769473a23570?w=500",
            Category = "Skate",
            Sizes = new List<string> { "7", "8", "9", "10", "11", "12" },
            Colors = new List<string> { "Black", "Navy", "Red" },
            Stock = 75,
            Rating = 4.5m,
            ReviewCount = 278
        },
        new Shoe
        {
            Id = 8,
            Name = "Zoom Pegasus 39",
            Brand = "Nike",
            Description = "Responsive running shoes with Zoom Air cushioning.",
            Price = 119.99m,
            ImageUrl = "https://images.unsplash.com/photo-1608231387042-66d1773070a5?w=500",
            Category = "Running",
            Sizes = new List<string> { "7", "8", "9", "10", "11" },
            Colors = new List<string> { "White", "Black", "Pink" },
            Stock = 40,
            Rating = 4.6m,
            ReviewCount = 201
        },
        new Shoe
        {
            Id = 9,
            Name = "574 Core",
            Brand = "New Balance",
            Description = "Classic running-inspired lifestyle sneakers.",
            Price = 89.99m,
            ImageUrl = "https://images.unsplash.com/photo-1600185365483-26d7a4cc7519?w=500",
            Category = "Casual",
            Sizes = new List<string> { "7", "8", "9", "10", "11", "12" },
            Colors = new List<string> { "Gray", "Navy", "Green" },
            Stock = 55,
            Rating = 4.4m,
            ReviewCount = 145
        },
        new Shoe
        {
            Id = 10,
            Name = "Yeezy Boost 350",
            Brand = "Adidas",
            Description = "Premium lifestyle sneakers with Primeknit upper.",
            Price = 219.99m,
            ImageUrl = "https://images.unsplash.com/photo-1560769629-975ec94e6a86?w=500",
            Category = "Lifestyle",
            Sizes = new List<string> { "7", "8", "9", "10", "11" },
            Colors = new List<string> { "Black", "White", "Gray" },
            Stock = 20,
            Rating = 4.9m,
            ReviewCount = 567
        }
    };

    public List<Shoe> GetAllShoes() => _shoes;

    public Shoe? GetShoeById(int id) => _shoes.FirstOrDefault(s => s.Id == id);

    public List<Shoe> GetShoesByCategory(string category) =>
        _shoes.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Shoe> SearchShoes(string searchTerm) =>
        _shoes.Where(s =>
            s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            s.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();

    public List<string> GetCategories() =>
        _shoes.Select(s => s.Category).Distinct().ToList();
}

