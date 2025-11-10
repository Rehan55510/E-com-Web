using E_com_Web.Models;

namespace E_com_Web.Services;

public class CartService : ICartService
{
    public CartViewModel GetCart(Dictionary<int, CartItem> cartItems)
    {
        return new CartViewModel
        {
            Items = cartItems.Values.ToList()
        };
    }

    public void AddToCart(Dictionary<int, CartItem> cartItems, Shoe shoe, string size, string color, int quantity)
    {
        var key = shoe.Id;
        
        if (cartItems.ContainsKey(key))
        {
            cartItems[key].Quantity += quantity;
        }
        else
        {
            cartItems[key] = new CartItem
            {
                ShoeId = shoe.Id,
                Name = shoe.Name,
                ImageUrl = shoe.ImageUrl,
                Price = shoe.Price,
                Size = size,
                Color = color,
                Quantity = quantity
            };
        }
    }

    public void RemoveFromCart(Dictionary<int, CartItem> cartItems, int shoeId)
    {
        cartItems.Remove(shoeId);
    }

    public void UpdateQuantity(Dictionary<int, CartItem> cartItems, int shoeId, int quantity)
    {
        if (cartItems.ContainsKey(shoeId))
        {
            if (quantity <= 0)
            {
                cartItems.Remove(shoeId);
            }
            else
            {
                cartItems[shoeId].Quantity = quantity;
            }
        }
    }
}

