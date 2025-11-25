namespace E_com_Web.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = "Pending"; // Pending, Processing, Completed, Shipped, Cancelled
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Failed, Refunded
    public string PaymentMethod { get; set; } = string.Empty; // CreditCard, DebitCard, COD
    
    // Navigation properties
    public OrderCustomer Customer { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = new();
}
