# System Architecture — Nexora

## Architecture Pattern
ASP.NET Core MVC (Model-View-Controller) with Entity Framework Core Code-First.

## Stack
| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core MVC (.NET 8 LTS) |
| ORM | Entity Framework Core 8 |
| Database | PostgreSQL (Neon hosting) |
| Auth | ASP.NET Core Identity |
| CSS | Tailwind CSS 3 + DaisyUI 4 |
| Client State | Alpine.js 3 |
| Server Calls | HTMX 2.0 |
| Images | Cloudinary (.NET SDK) |
| Font | Be Vietnam Pro (Google Fonts) |
| Icons | Heroicons (inline SVG) |
| Deploy | Railway |

## Database Schema

### Tables
- **AspNetUsers** (Identity + custom: FullName, Phone, Address, Avatar, CreatedAt, IsActive)
- **AspNetRoles** (Admin, Staff, Customer)
- **Category** (Id, Name, Slug, Description, ImagePath, SortOrder, IsActive)
- **Product** (Id, Name, Slug, Description, Price, OriginalPrice, Brand, Color, Storage?, RAM?, ScreenSize?, CPU?, CategoryId, IsActive, IsFeatured)
- **ProductImage** (Id, ProductId, ImagePath, SortOrder, IsMain)
- **Cart** (Id, UserId, CreatedAt) — 1:1 with User
- **CartDetail** (Id, CartId, ProductId, Quantity, UnitPrice)
- **Order** (Id, UserId, OrderCode, FullName, Phone, Address, Note, TotalAmount, Status, PaymentMethod)
- **OrderDetail** (Id, OrderId, ProductId, ProductName, Quantity, UnitPrice)
- **Voucher** (Id, Code, Description, DiscountPercent, DiscountAmount, MinOrderAmount, MaxDiscountAmount, UsageLimit, UsedCount, StartDate, EndDate, IsActive)
- **ShopConfig** (Id, Key, Value, Type)

### Relationships
- Category 1:N Product
- Product 1:N ProductImage
- User 1:1 Cart → Cart 1:N CartDetail → CartDetail N:1 Product
- User 1:N Order → Order 1:N OrderDetail → OrderDetail N:1 Product

## Authentication & Authorization
- 3 roles: Admin, Staff, Customer
- Cookie-based auth via ASP.NET Core Identity
- Role-based access: `[Authorize(Roles = "Admin")]`, `[Authorize(Roles = "Admin,Staff")]`
- Login required to add to cart

## Project Structure
```
Nexora/
├── Controllers/           # 11 controllers
│   ├── HomeController     # Homepage
│   ├── AccountController  # Login/Register/Logout
│   ├── ProductController  # Customer product browsing
│   ├── CartController     # Shopping cart
│   ├── OrderController    # Checkout, order history
│   ├── AdminController    # Dashboard
│   ├── AdminProductController  # Product CRUD
│   ├── AdminCategoryController # Category CRUD
│   ├── AdminOrderController    # Order management
│   ├── AdminVoucherController  # Voucher CRUD (Admin only)
│   ├── AdminUserController     # User management
│   └── AdminConfigController   # Shop config
├── Models/                # Entity models (10 entities)
│   └── ViewModels/        # Form/display models
├── Services/              # CloudinaryService (image upload)
├── Data/                  # DbContext, SeedData
├── Views/                 # Razor views (per controller)
│   └── Shared/            # Layouts, partials
└── wwwroot/               # Static files (CSS, JS)
```

## Layouts
- `_Layout.cshtml` — Customer-facing (navbar, footer, theme toggle)
- `_AdminLayout.cshtml` — Admin panel (sidebar + topbar)
