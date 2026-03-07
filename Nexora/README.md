# Nexora — Tech Ecommerce

Next-gen tech, delivered. E-commerce website for tech products built with ASP.NET Core MVC.

## Tech Stack

- **Backend:** ASP.NET Core MVC (.NET 8)
- **ORM:** Entity Framework Core + PostgreSQL (Neon)
- **Auth:** ASP.NET Core Identity (Admin/Staff/Customer)
- **CSS:** Tailwind CSS 3 + DaisyUI 4
- **Client:** Alpine.js + HTMX
- **Images:** Cloudinary

## Setup

```bash
# Install dependencies
dotnet restore
pnpm install

# Configure database
# Edit appsettings.json with your Neon PostgreSQL connection string

# Build CSS
pnpm run css:build

# Run migrations
dotnet ef database update

# Run
dotnet watch run
```

## Default Accounts

| Role     | Email              | Password     |
| -------- | ------------------ | ------------ |
| Admin    | admin@nexora.vn    | Admin@123    |
| Staff    | staff@nexora.vn    | Staff@123    |
| Customer | customer@nexora.vn | Customer@123 |

## Project Structure

```
Nexora/
├── Controllers/       # MVC Controllers
├── Data/              # DbContext, SeedData
├── Models/            # Entity models + ViewModels
├── Views/             # Razor views
│   ├── Shared/        # Layouts, partials
│   ├── Home/          # Homepage
│   ├── Product/       # Product listing/detail
│   ├── Cart/          # Shopping cart
│   ├── Order/         # Checkout, order history
│   ├── Account/       # Login, register
│   ├── Admin/         # Dashboard
│   ├── AdminProduct/  # Product CRUD
│   ├── AdminCategory/ # Category CRUD
│   └── AdminOrder/    # Order management
└── wwwroot/           # Static files
```
