# Code Standards — Nexora

## Language & Framework
- C# 12 / .NET 8 LTS
- ASP.NET Core MVC pattern
- Entity Framework Core Code-First

## Naming Conventions
| Element | Convention | Example |
|---------|-----------|---------|
| Classes | PascalCase | `ProductController`, `ApplicationUser` |
| Methods | PascalCase | `GetOrCreateCartAsync()` |
| Properties | PascalCase | `FullName`, `IsActive` |
| Local vars | camelCase | `cartDetails`, `totalItems` |
| Files | PascalCase (C#) | `AdminProductController.cs` |
| Views | PascalCase | `MyOrders.cshtml` |
| CSS classes | kebab-case (Tailwind) | `btn-primary`, `bg-base-200` |

## File Organization
- 1 class per file
- Models in `Models/`, ViewModels in `Models/ViewModels/`
- Controllers grouped by feature (Admin prefix for admin controllers)
- Views in folder matching controller name

## Database
- Code-First with Data Annotations + Fluent API
- Slug fields: unique index
- Soft delete via `IsActive` flag (not hard delete)
- Nullable spec fields for product variants
- Decimal columns: `decimal(18,0)` for VND (no decimals)

## Authentication
- ASP.NET Core Identity (not custom auth)
- 3 roles seeded: Admin, Staff, Customer
- `[Authorize]` on all cart/order actions
- `[Authorize(Roles = "Admin,Staff")]` on admin controllers
- `[Authorize(Roles = "Admin")]` on user/config/voucher management

## Frontend
- Tailwind CSS 3 + DaisyUI 4 (no Bootstrap)
- Alpine.js for client-side reactivity
- HTMX for partial page updates
- DaisyUI custom themes: `nexora-dark`, `nexora-light`
- Theme toggle persisted via localStorage

## Error Handling
- ModelState validation on all forms
- `[ValidateAntiForgeryToken]` on all POST actions
- Null checks with early return (`NotFound()`)
- Try-catch only at boundary layers
