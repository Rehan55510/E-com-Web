namespace E_com_Web.Models;

public class OrderCustomer
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    
    // Full name helper
    public string FullName => $"{FirstName} {LastName}";
    
    // Full address helper
    public string FullAddress => $"{Address}, {City}, {State} {ZipCode}, {Country}";
    
    // Navigation
    public Order Order { get; set; } = null!;
}
