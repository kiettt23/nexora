# Project Overview — Nexora

## Summary
Nexora is a tech ecommerce website built with ASP.NET Core MVC (.NET 8). Sells phones, laptops, tablets, and accessories. Dual purpose: C# assignment (grade A target) + real product for non-tech users.

## Brand
- **Name:** Nexora
- **Tagline:** "Next-gen tech, delivered."
- **Style:** Dark Tech + Premium

## Target Users
- **Customers:** Non-tech users buying tech products online
- **Admin/Staff:** Shop owners managing products, orders, configs

## Core Features
- Product browsing with filter/sort/search (HTMX live search)
- Shopping cart (DB-based, login required)
- Checkout with COD payment
- Order tracking with status timeline
- Admin dashboard with stats
- Product/Category/Order/User CRUD
- Dark/Light theme toggle
- Responsive design (mobile-first)

## Key Constraints
- No voucher system
- No guest cart (login required to buy)
- No wishlist, no email confirmation
- Payment: COD only
- Images hosted on Cloudinary (survives redeploy)
- Auth via ASP.NET Core Identity (not custom tables)

## Deliverables
1. `BaoCao/` — Report document (ERD, Usecase, project structure)
2. `Source/` — Full application source code
3. `Database/` — SQL migration script
