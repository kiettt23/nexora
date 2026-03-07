# Phase 5 — Product Module

## Priority: HIGH
## Status: Pending

## Overview
Customer-facing product browsing + Staff/Admin CRUD product management.

## Customer Features
1. **Product listing page** (`/products`)
   - Grid/list view toggle (Alpine.js)
   - Filter by: category, brand, price range, color
   - Sort by: price asc/desc, newest, name
   - Search by name (HTMX live search with debounce)
   - Pagination (HTMX partial reload)
2. **Product detail page** (`/products/{slug}`)
   - Image gallery (multiple images, click to zoom)
   - Specs table
   - Price (show discount if OriginalPrice exists)
   - Add to cart button
   - Related products (same category)

## Staff/Admin Features
3. **Product management** (`/admin/products`)
   - List all products (table with search/filter)
   - Create product (form + Dropzone.js image upload)
   - Edit product (pre-filled form + manage images)
   - Delete product (soft delete via IsActive)
4. **Category management** (`/admin/categories`)
   - CRUD categories

## Implementation Steps
1. Create ProductController (customer-facing)
2. Create Admin/ProductController (staff/admin CRUD)
3. Create Admin/CategoryController
4. Create product listing view with HTMX filter/search/pagination
5. Create product detail view
6. Create admin product form (Dropzone.js upload)
7. Create admin category form
8. Implement image upload to wwwroot/images/products/
9. Create ProductService for business logic (DI)

## Todo
- [ ] ProductController (listing, detail)
- [ ] HTMX filter/search/pagination
- [ ] Product detail page (gallery, specs, related)
- [ ] Admin ProductController (CRUD)
- [ ] Admin CategoryController (CRUD)
- [ ] Image upload with Dropzone.js
- [ ] ProductService (DI)

## Success Criteria
- Products filterable by category/brand/price
- Search works without page reload (HTMX)
- Product detail shows all info + image gallery
- Admin can create/edit/delete products with image upload
- Images stored in wwwroot/images/products/
