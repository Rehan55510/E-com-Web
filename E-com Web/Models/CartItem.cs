namespace E_com_Web.Models;

public class CartItem
{
    public int ShoeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Quantity { get; set; }
    
    public decimal Total => Price * Quantity;
}

