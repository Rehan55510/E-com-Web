using E_com_Web.Models;

namespace E_com_Web.Services;

public interface ICartService
{
    CartViewModel GetCart(Dictionary<int, CartItem> cartItems);
    void AddToCart(Dictionary<int, CartItem> cartItems, Shoe shoe, string size, string color, int quantity);
    void RemoveFromCart(Dictionary<int, CartItem> cartItems, int shoeId);
    void UpdateQuantity(Dictionary<int, CartItem> cartItems, int shoeId, int quantity);
}

