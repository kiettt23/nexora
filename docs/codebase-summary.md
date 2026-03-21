# Codebase Summary — Nexora

## Controllers (12 total — Areas-based)

### Customer Controllers (Controllers/)
| Controller | Role | Key Actions |
|-----------|------|-------------|
| HomeController | Public | Index (homepage) |
| AccountController | Public | Login, Register, Logout, AccessDenied, Profile |
| ProductController | Public | Index (listing + filter/sort/search), Detail |
| CartController | Customer | Index, Add, Update, Remove, Count |
| OrderController | Customer | Checkout (GET/POST), Success, MyOrders, Detail |

### Admin Area Controllers (Areas/Admin/Controllers/)
| Controller | Role | Key Actions |
|-----------|------|-------------|
| DashboardController | Staff/Admin | Index (dashboard with stats) |
| ProductController | Staff/Admin | Index, Create, Edit, ToggleActive |
| CategoryController | Staff/Admin | Index, Create, Edit |
| OrderController | Staff/Admin | Index (filter by status), Detail, UpdateStatus |
| VoucherController | Admin | Index, Create, Edit, Delete |
| UserController | Admin | Index, ToggleActive, ChangeRole |
| ConfigController | Admin | Index, Update, Add |

## Models (10 entities)

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
| Voucher | Code, DiscountPercent, DiscountAmount, MinOrderAmount, MaxDiscountAmount, UsageLimit, UsedCount, StartDate, EndDate, IsActive |
| ShopConfig | Key, Value, Type |

## ViewModels (4)
- LoginViewModel, RegisterViewModel, CheckoutViewModel, ProductFormViewModel

## Services (1)
- `CloudinaryService` — Upload/delete images via Cloudinary SDK (IFormFile → Cloudinary → URL)

## Views Structure
- 2 layouts: `Views/Shared/_Layout.cshtml` (customer), `Areas/Admin/Views/Shared/_AdminLayout.cshtml` (admin)
- 3 shared partials: `_ProductCard`, `_Pagination`, `_Toast`
- Customer views in `Views/` (5 folders), Admin views in `Areas/Admin/Views/` (7 folders)

## Data Layer
- `ApplicationDbContext` — EF Core DbContext with Fluent API
- `SeedData` — Seeds 3 users (admin/staff/customer), 4 categories, 4 shop configs
