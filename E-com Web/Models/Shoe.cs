namespace E_com_Web.Models;

public class Shoe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<string> Sizes { get; set; } = new();
    public List<string> Colors { get; set; } = new();
    public int Stock { get; set; }
    public decimal? Rating { get; set; }
    public int? ReviewCount { get; set; }
}

