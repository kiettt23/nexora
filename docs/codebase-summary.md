# Codebase Summary — Nexora

## Controllers (9 total)

| Controller | Role | Key Actions |
|-----------|------|-------------|
| HomeController | Public | Index (homepage) |
| AccountController | Public | Login, Register, Logout, AccessDenied |
| ProductController | Public | Index (listing + filter/sort/search), Detail |
| CartController | Customer | Index, Add, Update, Remove, Count |
| OrderController | Customer | Checkout (GET/POST), Success, MyOrders, Detail |
| AdminController | Staff/Admin | Index (dashboard with stats) |
| AdminProductController | Staff/Admin | Index, Create, Edit, ToggleActive |
| AdminCategoryController | Staff/Admin | Index, Create, Edit |
| AdminOrderController | Staff/Admin | Index (filter by status), Detail, UpdateStatus |
| AdminUserController | Admin | Index, ToggleActive, ChangeRole |
| AdminConfigController | Admin | Index, Update, Add |

## Models (9 entities)

| Model | Key Fields |
|-------|-----------|
| ApplicationUser | FullName, Phone, Address, Avatar, IsActive (extends IdentityUser) |
| Category | Name, Slug, Description, ImagePath, SortOrder, IsActive |
| Product | Name, Slug, Price, OriginalPrice, Brand, RAM?, Storage?, CPU?, ScreenSize?, IsFeatured |
| ProductImage | ImagePath, SortOrder, IsMain |
| Cart | UserId (1:1 with user) |
| CartDetail | Quantity, UnitPrice |
| Order | OrderCode, FullName, Phone, Address, TotalAmount, Status, PaymentMethod |
| OrderDetail | ProductName, Quantity, UnitPrice |
| ShopConfig | Key, Value, Type |

## ViewModels (4)
- LoginViewModel, RegisterViewModel, CheckoutViewModel, ProductFormViewModel

## Views Structure
- 2 layouts: `_Layout.cshtml` (customer), `_AdminLayout.cshtml` (admin)
- 3 shared partials: `_ProductCard`, `_Pagination`, `_Toast`
- ~25 view files across 11 view folders

## Data Layer
- `ApplicationDbContext` — EF Core DbContext with Fluent API
- `SeedData` — Seeds 3 users (admin/staff/customer), 4 categories, 4 shop configs
