# Phase 7 — Admin Panel

## Priority: MEDIUM
## Status: Pending

## Overview
Admin dashboard, user management, shop config, and statistics.

## Admin Features
1. **Dashboard** (`/admin`)
   - Total revenue, total orders, total products, total customers (cards)
   - Revenue chart (last 7/30 days — simple bar/line chart)
   - Top selling products
   - Recent orders
2. **User management** (`/admin/users`)
   - List all users (filter by role)
   - Create staff account
   - Edit user (change role, activate/deactivate)
   - Cannot delete admin self
3. **Shop config** (`/admin/config`)
   - Edit shop name, logo, banners, phone, email, address, social links
   - Upload logo/banners (Dropzone.js)
   - Changes reflect immediately on customer-facing site
4. **Statistics/Reports** (`/admin/reports`)
   - Revenue by period
   - Top products by quantity sold
   - Orders by status

## Implementation Steps
1. Create AdminController (dashboard)
2. Create Admin/UserController (CRUD staff)
3. Create Admin/ConfigController (shop settings)
4. Create Admin/ReportController (statistics)
5. Dashboard charts: use Chart.js (CDN) or simple HTML/CSS bars
6. ShopConfigService: load config, cache in memory, invalidate on update
7. _Layout.cshtml reads ShopConfig for shop name, logo, footer info

## Todo
- [ ] Admin dashboard (stats cards, chart, recent orders)
- [ ] User management (list, create staff, edit role)
- [ ] Shop config page (edit + upload)
- [ ] ShopConfigService (cached config)
- [ ] Statistics/Reports page
- [ ] Chart.js integration

## Success Criteria
- Dashboard shows real data
- Admin can create/edit staff accounts
- Shop config changes reflect on frontend immediately
- Statistics display revenue and top products
