namespace E_com_Web.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ShoeId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal Subtotal { get; set; }
    
    // Navigation
    public Order Order { get; set; } = null!;
}
