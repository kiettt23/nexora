# Phase 2 — Database Design

## Priority: HIGH
## Status: Pending

## Overview
Design and implement database schema for Nexora ecommerce using EF Core Code-First with PostgreSQL.

## Key Insights
- ASP.NET Core Identity auto-creates user/role tables (AspNetUsers, AspNetRoles, etc.)
- Extend IdentityUser with custom fields (FullName, Phone, Address, Avatar)
- Use Data Annotations + Fluent API (required by assignment)
- Seed data: 3 users (1 admin, 1 staff, 1 customer) + 4 categories

## Database Schema

### Tables

**ApplicationUser** (extends IdentityUser)
- FullName, Phone, Address, Avatar, CreatedAt, IsActive

**Category**
- Id, Name, Slug, Description, ImagePath, SortOrder, IsActive, CreatedAt

**Product**
- Id, Name, Slug, Description, Price, OriginalPrice, Brand, Color, Storage?, RAM?, ScreenSize?, CPU?, CategoryId (FK), IsActive, IsFeatured, CreatedAt, UpdatedAt
- Note: Storage, RAM, ScreenSize, CPU are NULLABLE (accessories don't have these specs)
<!-- Updated: Validation Session 1 - Spec fields nullable for accessories -->

**ProductImage**
- Id, ProductId (FK), ImagePath, SortOrder, IsMain

**Cart**
- Id, UserId (FK), CreatedAt, UpdatedAt

**CartDetail**
- Id, CartId (FK), ProductId (FK), Quantity, UnitPrice

**Order**
- Id, UserId (FK), OrderCode, FullName, Phone, Address, Note, TotalAmount, Status (Pending/Confirmed/Shipping/Delivered/Cancelled), PaymentMethod, CreatedAt, UpdatedAt

**OrderDetail**
- Id, OrderId (FK), ProductId (FK), ProductName, Quantity, UnitPrice

**ShopConfig**
- Id, Key, Value, Type (string/image/text)
  - Keys: ShopName, Logo, Banner1, Banner2, Banner3, Phone, Email, Address, Facebook, Zalo

### Relationships
- Category 1:N Product
- Product 1:N ProductImage
- User 1:1 Cart
- Cart 1:N CartDetail
- CartDetail N:1 Product
- User 1:N Order
- Order 1:N OrderDetail
- OrderDetail N:1 Product

## Implementation Steps
1. Create Models (Data Annotations)
2. Create ApplicationDbContext (Fluent API configurations)
3. Create seed data (admin, staff, customer, categories)
4. Generate migration
5. Apply migration to Neon DB
6. Verify tables created

## Todo
- [ ] Create model classes (Models/)
- [ ] Configure ApplicationDbContext with Fluent API
- [ ] Create seed data
- [ ] Generate and apply migration
- [ ] Verify DB schema

## Success Criteria
- All tables created in PostgreSQL
- Relationships and constraints correct
- Seed data inserted (3 users, 4 categories)
- EF Core queries work (LINQ to Entities)
