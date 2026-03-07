# Phase 3 — Auth & Identity

## Priority: HIGH
## Status: Pending

## Overview
Implement authentication and authorization using ASP.NET Core Identity with 3 roles.

## Roles
- **Admin** — Manage staff, shop config, dashboard
- **Staff** — CRUD products, manage orders
- **Customer** — Browse, cart, order, profile

## Implementation Steps
1. Configure Identity in Program.cs (cookie auth, login/logout paths)
2. Create AccountController:
   - GET/POST Register (Customer only)
   - GET/POST Login
   - POST Logout
3. Create Register view (form: email, password, fullname, phone)
4. Create Login view (form: email, password, remember me)
5. Add `[Authorize]` attributes on controllers
6. Add `[Authorize(Roles = "Admin")]` for admin controllers
7. Add `[Authorize(Roles = "Staff,Admin")]` for staff controllers
8. Create role-based navigation (show/hide menu items)
9. Session/Cookie management (required by assignment)
10. Seed default admin account (admin@nexora.com / Admin@123)

## Todo
- [ ] Configure Identity in Program.cs
- [ ] Create AccountController (Register, Login, Logout)
- [ ] Create Register/Login views with DaisyUI forms
- [ ] Setup role-based authorization
- [ ] Seed admin + staff accounts
- [ ] Role-based nav menu

## Success Criteria
- Register creates Customer account
- Login works with cookie auth
- Admin pages blocked for non-admin
- Staff pages blocked for customers
- Logout clears session
