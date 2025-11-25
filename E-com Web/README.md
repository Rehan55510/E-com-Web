# 👟 E-Commerce Shoe Store

A modern, full-featured e-commerce web application built with ASP.NET Core MVC, featuring real-time order management, comprehensive admin panel, and fully responsive design.

## 🚀 Features

### Customer-Facing Features
- **Modern Homepage** - Hero section, category browsing, featured products
- **Product Catalog** - Search, filter by category, detailed product pages
- **Shopping Cart** - Add/remove items, quantity management, session-based storage
- **Secure Checkout** - Complete order processing with customer information capture
- **Fully Responsive** - Mobile-first design works on all devices (320px - 4K)

### Admin Panel Features
- **Order Management**
  - View all orders with filtering by status
  - Complete order details with customer information
  - Update order status (Pending → Processing → Completed → Shipped)
  - Real-time order notifications via SignalR
  
- **Product Management**
  - Add/Edit/Delete products
  - Image upload with auto-save to server
  - Manage inventory, pricing, categories
  - Toggle products visibility on homepage (ShowOnHome flag)
  
- **Real-Time Analytics Dashboard**
  - Total orders count
  - Total revenue tracking
  - Orders breakdown by status
  - Recent orders list
  - Low stock alerts
  - **NO FAKE DATA** - All metrics from actual database

- **REST API Endpoints**
  - `GET /api/admin/orders` - Paginated orders list
  - `GET /api/admin/orders/{id}` - Full order details
  - `PUT /api/admin/orders/{id}/status` - Update order status
  - `GET /api/admin/products/home` - Homepage products
  - `GET /api/admin/analytics` - Real-time analytics data

## 🛠️ Technology Stack

- **Framework:** ASP.NET Core 8.0 MVC
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Real-Time:** SignalR
- **Authentication:** Cookie-based authentication
- **Frontend:** HTML5, CSS3 (Responsive), Vanilla JavaScript
- **Design:** Custom CSS with mobile-first approach

## 📋 Prerequisites

- .NET 8.0 SDK or later
- SQL Server 2019 or later (Express/Developer/Enterprise)
- Visual Studio 2022+ OR VS Code with C# extension
- Git (for cloning)

## 🔧 Installation & Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd "E-com Web"
```

### 2. Configure Database Connection

Update `appsettings.json` with your SQL Server connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EcomShoeStore;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

This creates the database and all required tables.

### 4. Run the Application

```bash
dotnet run
```

Access the application:
- **Customer Site:** https://localhost:5001
- **Admin Panel:** https://localhost:5001/Admin/Dashboard

### 5. Admin Login Credentials

```
Username: admin
Password: ChangeMe_123!
```

**⚠️ IMPORTANT:** Change these credentials in production!

## 🆕 Recent Updates

### November 2024
- ✅ Added SignalR real-time order notifications
- ✅ Implemented REST API endpoints for admin
- ✅ Fixed product creation with image upload
- ✅ Added comprehensive responsive design
- ✅ Implemented ShowOnHome flag for products
- ✅ Created real-time analytics dashboard
- ✅ Removed all fake/dummy data

## 📞 Support

Developed by **Rehan** 

---

**⭐ Star this repo if you find it helpful!**
