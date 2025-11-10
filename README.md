# ShoeStore - E-commerce Web Application

A modern, full-featured e-commerce web application for selling shoes, built with ASP.NET Core MVC.

## Features

- 🛍️ **Product Catalog**: Browse shoes by category with search functionality
- 🔍 **Product Search**: Search shoes by name, brand, or description
- 📦 **Shopping Cart**: Add items to cart with size and color selection
- 💳 **Checkout**: Complete checkout process with shipping and payment information
- 🎨 **Modern UI**: Beautiful, responsive design that works on all devices
- ⭐ **Product Ratings**: View product ratings and reviews
- 🚚 **Features Display**: Free shipping, easy returns, secure payment, quality guarantee

## Technologies Used

- ASP.NET Core 8.0 (MVC)
- C# 12
- HTML5, CSS3, JavaScript
- Session-based shopping cart
- Responsive design

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- A code editor (Visual Studio, VS Code, or Rider)

### Running the Application

1. Navigate to the project directory:
   ```bash
   cd "E-com Web"
   ```

2. Restore dependencies (if needed):
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`

### Project Structure

```
E-com Web/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   ├── ProductsController.cs
│   ├── CartController.cs
│   └── CheckoutController.cs
├── Models/              # Data Models
│   ├── Shoe.cs
│   ├── CartItem.cs
│   ├── CartViewModel.cs
│   └── CheckoutViewModel.cs
├── Services/            # Business Logic Services
│   ├── IShoeService.cs
│   ├── ShoeService.cs
│   ├── ICartService.cs
│   └── CartService.cs
├── Views/               # Razor Views
│   ├── Home/
│   ├── Products/
│   ├── Cart/
│   └── Checkout/
├── wwwroot/            # Static Files
│   ├── css/
│   └── js/
└── Program.cs          # Application Entry Point
```

## Features in Detail

### Product Catalog
- View all available shoes
- Filter by category (Running, Casual, Skate, Lifestyle)
- Search functionality
- Product details with images, prices, ratings

### Shopping Cart
- Add items with size and color selection
- Update quantities
- Remove items
- View cart total with tax calculation
- Session-based cart storage

### Checkout
- Shipping information form
- Payment information form
- Order summary
- Order confirmation

## Sample Products

The application comes with 10 pre-loaded shoe products from popular brands:
- Nike (Air Max 90, Air Force 1, Zoom Pegasus 39)
- Adidas (Ultraboost 22, Yeezy Boost 350)
- Converse (Chuck Taylor All Star)
- Reebok (Classic Leather)
- ASICS (Gel-Kayano 29)
- Vans (Old Skool)
- New Balance (574 Core)

## Future Enhancements

- User authentication and accounts
- Order history
- Product reviews and ratings system
- Payment gateway integration
- Admin panel for product management
- Database integration (currently using in-memory data)
- Image upload functionality
- Wishlist feature
- Product recommendations

## License

This project is open source and available for educational purposes.

## Author

Created as a modern e-commerce solution for shoe retail.

