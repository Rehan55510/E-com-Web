namespace E_com_Web.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public decimal Subtotal => Items.Sum(i => i.Total);
    public decimal Tax => Subtotal * 0.1m; // 10% tax
    public decimal Total => Subtotal + Tax;
}

