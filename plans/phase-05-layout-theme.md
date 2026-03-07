# Phase 4 — Layout & Theme

## Priority: HIGH
## Status: Pending

## Overview
Create unified layout, dark/light theme toggle, responsive navbar, footer, and homepage.

## Design Specs
- **Font:** Be Vietnam Pro
- **Colors Dark:** BG #0F172A / Text #F8FAFC / Gold #F59E0B / Border #334155
- **Colors Light:** BG #FFFFFF / Text #1E293B / Gold #F59E0B / Border #E5E7EB
- **DaisyUI themes:** Custom "nexora-dark" and "nexora-light"
- **Icons:** Heroicons (inline SVG)

## Implementation Steps
1. Configure DaisyUI custom themes in tailwind.config.js
2. Create _Layout.cshtml:
   - Floating navbar (logo, search, cart icon, user menu, theme toggle)
   - Footer (shop info from ShopConfig, social links, categories)
   - Alpine.js theme toggle (localStorage persistence)
3. Create Homepage sections:
   - Hero banner (from ShopConfig banners)
   - Category cards (4 categories with icons)
   - Featured products grid (IsFeatured = true)
   - Deals section (products with OriginalPrice > Price)
   - Newsletter signup (optional)
4. Create shared partials:
   - _ProductCard.cshtml (reusable product card)
   - _Pagination.cshtml
   - _Breadcrumb.cshtml
   - _Toast.cshtml (Alpine.js notification)
5. Responsive: 375px, 768px, 1024px, 1440px
6. Admin layout (_AdminLayout.cshtml): sidebar + content area

## Todo
- [ ] DaisyUI custom themes (nexora-dark, nexora-light)
- [ ] _Layout.cshtml (navbar, footer, theme toggle)
- [ ] Homepage (hero, categories, featured, deals)
- [ ] Shared partials (product card, pagination, toast)
- [ ] Admin layout (sidebar + content)
- [ ] Responsive testing

## Success Criteria
- Theme toggle works (persists via localStorage)
- Navbar responsive (hamburger on mobile)
- Homepage renders all sections
- Admin layout separate from customer layout
- Consistent look across all pages
