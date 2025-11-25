using Microsoft.AspNetCore.SignalR;

namespace E_com_Web.Infrastructure.SignalR;

public class OrderHub : Hub
{
    // Method to notify all admins of a new order
    public async Task NotifyNewOrder(int orderId, string customerName, decimal totalAmount)
    {
        await Clients.Group("Admins").SendAsync("ReceiveNewOrder", orderId, customerName, totalAmount);
    }

    // Method to notify all admins when order status changes
    public async Task NotifyOrderStatusChanged(int orderId, string newStatus)
    {
        await Clients.Group("Admins").SendAsync("ReceiveOrderStatusChange", orderId, newStatus);
    }

    // Called when admin connects
    public override async Task OnConnectedAsync()
    {
        // Add admin users to "Admins" group
        if (Context.User?.IsInRole("Admin") == true || Context.User?.HasClaim("Role", "Admin") == true)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }
        await base.OnConnectedAsync();
    }

    // Called when admin disconnects
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
        await base.OnDisconnectedAsync(exception);
    }
}
