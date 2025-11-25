using E_com_Web.Models;

namespace E_com_Web.Areas.Admin.Models;

public class DashboardStats
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public Dictionary<string, int> OrdersByStatus { get; set; } = new();
    public IEnumerable<Order> RecentOrders { get; set; } = new List<Order>();
    public int TotalProducts { get; set; }
    public int LowStockProducts { get; set; }
}
