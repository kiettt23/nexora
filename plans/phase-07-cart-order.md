# Phase 7 — Cart & Order

## Priority: HIGH
## Status: Pending

## Overview
Shopping cart (DB-based, login required) + order placement + order management. Payment: COD only.
<!-- Updated: Validation Session 1 - Removed guest cart/session. Login required to add to cart. -->

## Customer Features
1. **Cart** (`/cart`) — requires [Authorize]
   - Add to cart (HTMX, no reload, update cart badge)
   - View cart (product list, quantity, subtotal, total)
   - Update quantity (HTMX)
   - Remove item (HTMX)
   - Cart badge in navbar (Alpine.js reactive count)
   - Guest clicks "Add to cart" → redirect to login
2. **Checkout** (`/checkout`)
   - Form: fullname, phone, address, note
   - Payment: COD only
   - Order summary
   - Place order button
3. **Order history** (`/orders`)
   - List past orders (code, date, status, total)
   - Order detail (items, status timeline)

## Staff/Admin Features
4. **Order management** (`/admin/orders`)
   - List all orders (filter by status)
   - View order detail
   - Update order status (Pending → Confirmed → Shipping → Delivered / Cancelled)

## Implementation Steps
1. Create CartService (add, remove, update, get) — DB only via Cart + CartDetail tables
2. Create CartController ([Authorize])
3. Create OrderController (customer)
4. Create Admin/OrderController (staff/admin)
5. Create checkout flow (cart → checkout form → confirm → order created)
6. Generate unique OrderCode (e.g., NX-20260306-001)
7. Cart badge: Alpine.js store synced with server count

## Todo
- [ ] CartService (DB-based, login required)
- [ ] CartController (add, remove, update, view) [Authorize]
- [ ] HTMX cart interactions (no page reload)
- [ ] Guest "Add to cart" → redirect login
- [ ] Cart badge in navbar (Alpine store)
- [ ] Checkout page + order placement (COD)
- [ ] OrderController (history, detail)
- [ ] Admin OrderController (list, status update)
- [ ] Order code generation

## Success Criteria
- Add to cart works without reload (logged-in users)
- Guest clicks add to cart → redirect to login
- Cart persists in DB
- Checkout creates order (COD), clears cart
- Order history visible to customer
- Staff can update order status
