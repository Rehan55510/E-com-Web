using E_com_Web.Models;

namespace E_com_Web.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if database already has data
        if (context.Shoes.Any())
        {
            return; // Database has been seeded
        }

        var shoes = new List<Shoe>
        {
            // Running Shoes
            new Shoe
            {
                Name = "Air Max 90",
                Brand = "Nike",
                Description = "Classic running shoes with visible Air cushioning, offering comfort and style for everyday wear.",
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
                Name = "Ultraboost 22",
                Brand = "Adidas",
                Description = "Premium running shoes with responsive Boost cushioning for maximum energy return.",
                Price = 189.99m,
                ImageUrl = "https://images.unsplash.com/photo-1608231387042-66d1773070a5?w=500",
                Category = "Running",
                Sizes = new List<string> { "7", "8", "9", "10", "11", "12", "13" },
                Colors = new List<string> { "Core Black", "Cloud White", "Solar Red" },
                Stock = 35,
                Rating = 4.7m,
                ReviewCount = 456
            },
            new Shoe
            {
                Name = "990v5",
                Brand = "New Balance",
                Description = "American-made classic with ENCAP midsole technology for superior support and durability.",
                Price = 184.99m,
                ImageUrl = "https://images.unsplash.com/photo-1539185441755-769473a23570?w=500",
                Category = "Running",
                Sizes = new List<string> { "7", "8", "9", "10", "11", "12" },
                Colors = new List<string> { "Grey", "Navy", "Burgundy" },
                Stock = 28,
                Rating = 4.6m,
                ReviewCount = 189
            },

            // Basketball
            new Shoe
            {
                Name = "LeBron 21",
                Brand = "Nike",
                Description = "Elite basketball performance shoe with Zoom Air cushioning and supportive fit.",
                Price = 199.99m,
                ImageUrl = "https://images.unsplash.com/photo-1607522370275-f14206abe5d3?w=500",
                Category = "Basketball",
                Sizes = new List<string> { "8", "9", "10", "11", "12", "13", "14" },
                Colors = new List<string> { "Black", "Purple", "Gold" },
                Stock = 42,
                Rating = 4.8m,
                ReviewCount = 312
            },
            new Shoe
            {
                Name = "Harden Vol. 8",
                Brand = "Adidas",
                Description = "Signature basketball shoe designed for explosive moves and step-back jumpers.",
                Price = 149.99m,
                ImageUrl = "https://images.unsplash.com/photo-1579338559194-a162d19bf842?w=500",
                Category = "Basketball",
                Sizes = new List<string> { "8", "9", "10", "11", "12", "13" },
                Colors = new List<string> { "Black", "Red", "White" },
                Stock = 38,
                Rating = 4.4m,
                ReviewCount = 167
            },

            // Casual
            new Shoe
            {
                Name = "Old Skool",
                Brand = "Vans",
                Description = "Iconic skateboarding shoe with the classic side stripe, perfect for everyday casual wear.",
                Price = 64.99m,
                ImageUrl = "https://images.unsplash.com/photo-1525966222134-fcfa99b8ae77?w=500",
                Category = "Casual",
                Sizes = new List<string> { "6", "7", "8", "9", "10", "11", "12" },
                Colors = new List<string> { "Black", "Navy", "Checkerboard" },
                Stock = 75,
                Rating = 4.6m,
                ReviewCount = 892
            },
            new Shoe
            {
                Name = "Chuck Taylor All Star",
                Brand = "Converse",
                Description = "Timeless canvas sneaker that has been a cultural icon since 1917.",
                Price = 59.99m,
                ImageUrl = "https://images.unsplash.com/photo-1514989940723-e8e51635b782?w=500",
                Category = "Casual",
                Sizes = new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" },
                Colors = new List<string> { "Black", "White", "Red", "Navy" },
                Stock = 120,
                Rating = 4.5m,
                ReviewCount = 1543
            },

            // Lifestyle
            new Shoe
            {
                Name = "Air Force 1 '07",
                Brand = "Nike",
                Description = "The legendary basketball sneaker that became a streetwear staple. Pure versatility.",
                Price = 109.99m,
                ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=500",
                Category = "Lifestyle",
                Sizes = new List<string> { "6", "7", "8", "9", "10", "11", "12", "13" },
                Colors = new List<string> { "White", "Black", "Triple White" },
                Stock = 95,
                Rating = 4.9m,
                ReviewCount = 2341
            },
            new Shoe
            {
                Name = "Stan Smith",
                Brand = "Adidas",
                Description = "Minimalist tennis-inspired sneaker with clean lines and eco-friendly materials.",
                Price = 89.99m,
                ImageUrl = "https://images.unsplash.com/photo-1560769629-975ec94e6a86?w=500",
                Category = "Lifestyle",
                Sizes = new List<string> { "6", "7", "8", "9", "10", "11", "12" },
                Colors = new List<string> { "White Green", "White Navy", "Triple White" },
                Stock = 68,
                Rating = 4.7m,
                ReviewCount = 1876
            },

            // Training
            new Shoe
            {
                Name = "Metcon 9",
                Brand = "Nike",
                Description = "Versatile training shoe built for lifting, running, and high-intensity workouts.",
                Price = 149.99m,
                ImageUrl = "https://images.unsplash.com/photo-1606107557195-0e29a4b5b4aa?w=500",
                Category = "Training",
                Sizes = new List<string> { "7", "8", "9", "10", "11", "12", "13" },
                Colors = new List<string> { "Black", "Volt", "Grey" },
                Stock = 44,
                Rating = 4.6m,
                ReviewCount = 278
            }
        };

        context.Shoes.AddRange(shoes);
        context.SaveChanges();
    }
}
