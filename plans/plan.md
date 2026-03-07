# Nexora — Tech Ecommerce (ASP.NET Core MVC)

## Overview
E-commerce website for tech products (phones, laptops, tablets, accessories).
Dual purpose: C# 3 assignment (grade A target) + real product for non-tech users.
Brand: **Nexora** — "Next-gen tech, delivered."

## Tech Stack
- **Backend:** ASP.NET Core MVC (.NET 8 LTS)
- **ORM:** Entity Framework Core + PostgreSQL (Neon)
- **Auth:** ASP.NET Core Identity (3 roles: Admin/Staff/Customer)
- **CSS:** Tailwind CSS + DaisyUI
- **Font:** Be Vietnam Pro (Google Fonts)
- **Icons:** Heroicons (SVG)
- **Client state:** Alpine.js
- **Server calls:** HTMX
- **File upload:** Dropzone.js or vanilla JS + Alpine
- **Image hosting:** Cloudinary (.NET SDK — CloudinaryDotNet)
- **Charts:** Chart.js (CDN)
- **Deploy:** Railway ($5 free/month)
- **Database hosting:** Neon PostgreSQL (free tier)
- **IDE:** VS Code

## Design System
- **Style:** Dark Tech + Premium
- **Theme:** Light + Dark (DaisyUI custom themes: nexora-dark, nexora-light)
- **Dark:** BG #0F172A / Text #F8FAFC / Gold #F59E0B / Border #334155
- **Light:** BG #FFFFFF / Text #1E293B / Gold #F59E0B / Border #E5E7EB
- **Animation:** 200-300ms, prefers-reduced-motion respected
- **Cards:** Rounded, subtle shadow, hover glow
- **Homepage:** Full Showcase (Hero → Categories → Featured → Deals → Newsletter)

## Phases

| # | Phase | Status |
|---|---|---|
| 1 | [Setup Environment](phase-01-setup-environment.md) | Done |
| 2 | [Database Design](phase-02-database-design.md) | Done |
| 3 | [Crawl Data](phase-03-crawl-data.md) | Done |
| 4 | [Auth & Identity](phase-04-auth-identity.md) | Done |
| 5 | [Layout & Theme](phase-05-layout-theme.md) | Done |
| 6 | [Product Module](phase-06-product-module.md) | Done |
| 7 | [Cart & Order](phase-07-cart-order.md) | Done |
| 8 | [Admin Panel](phase-08-admin-panel.md) | Done |
| 9 | [Polish & Deploy](phase-09-polish-deploy.md) | Done |
| 10 | [Report (Y5)](phase-10-report.md) | Pending |

## Products
- ~105 products across 4 categories (Phone, Laptop, Tablet, Accessories)
- Source: Crawl from Thegioididong.com
- Product specs: nullable fields (RAM, Storage, CPU, ScreenSize — null for accessories)

## Key Decisions
- **No voucher** — removed from scope
- **No guest cart** — must login to add to cart (guest can browse only)
- **No wishlist, no email confirmation**
- **Payment: COD only**
- **Images: Cloudinary** (not wwwroot — survives redeploy)
- **Auth: ASP.NET Core Identity** (not custom Account/Role tables)
- **Product specs: nullable columns** (not JSON, not separate tables)
- **Crawl data early** (after DB design, before UI work)

## Deliverables (Assignment)
1. `BaoCao/` — BaoCao.doc (ERD, Usecase, project structure)
2. `Source/` — Full application
3. `Database/` — .sql script

## Validation Log

### Session 1 — 2026-03-06
**Trigger:** Initial plan creation and validation
**Questions asked:** 6

#### Questions & Answers

1. **[Risk]** Image storage: wwwroot/images/ sẽ mất khi redeploy Docker trên Railway. Giải quyết thế nào?
   - Options: Supabase Storage | Railway Volume | wwwroot + chấp nhận
   - **Answer:** Cloudinary (user suggested UploadThing → không hỗ trợ C# → đổi sang Cloudinary)
   - **Rationale:** Cloudinary có .NET SDK chính thức, free 25GB bandwidth, ảnh persist qua redeploy

2. **[Architecture]** Product schema có RAM, Storage, CPU — phụ kiện không có. Xử lý thế nào?
   - Options: Nullable fields | JSON specs column | Tách bảng theo category
   - **Answer:** Nullable fields
   - **Rationale:** Đơn giản nhất, UI ẩn field null, không cần query JSONB phức tạp

3. **[Risk]** Phase 8 (Crawl) đặt cuối nhưng Phase 4-6 cần data test. Nên crawl sớm hơn?
   - Options: Crawl sau Phase 2 | Giữ Phase 8 | Crawl song song
   - **Answer:** Crawl sau Phase 2
   - **Rationale:** Có real data sớm giúp test UI/UX chính xác hơn fake data

4. **[Scope]** Thanh toán chỉ COD. Có cần thêm chuyển khoản/QR?
   - Options: Chỉ COD | COD + QR banking | Tính sau
   - **Answer:** Chỉ COD
   - **Rationale:** Đơn giản, đủ cho bài tập + shop nhỏ

5. **[Assumption]** Cart guest (session) + logged-in (DB) + merge on login. Có cần guest cart?
   - Options: Bắt buộc đăng nhập | Giữ guest cart + merge | Guest chỉ xem
   - **Answer:** Bắt buộc đăng nhập mới mua
   - **Rationale:** Bỏ session cart phức tạp, nhiều shop VN làm vậy

6. **[Scope]** Đề gợi ý bảng Account/Role riêng. Dùng Identity hay tự làm?
   - Options: Identity | Tự làm Account + Role | Hỏi giảng viên
   - **Answer:** Identity (đề không yêu cầu tự làm, chỉ gợi ý tên bảng)
   - **Rationale:** Identity chuyên nghiệp, bảo mật, tạo bảng User/Role tương đương

#### Confirmed Decisions
- Image hosting: Cloudinary (.NET SDK) — persist qua redeploy
- Product specs: nullable fields — đơn giản, UI ẩn null
- Crawl data: move to Phase 3 — có data sớm cho UI testing
- Payment: COD only — đơn giản
- Cart: login required — bỏ session cart complexity
- Auth: ASP.NET Core Identity — chuyên nghiệp, đề không cấm

#### Action Items
- [x] Update plan.md: reorder phases (crawl = Phase 3)
- [x] Update plan.md: image storage = Cloudinary
- [ ] Update phase-02: nullable spec fields in schema
- [ ] Update phase-06: remove guest cart/session logic
- [ ] Rename phase files to match new order
- [ ] Update phase-01: add Cloudinary NuGet package

#### Impact on Phases
- Phase 1: Add CloudinaryDotNet NuGet package + Cloudinary config
- Phase 2: Product spec fields (RAM, Storage, CPU, ScreenSize) = nullable
- Phase 3 (was 8): Crawl data moved up, runs right after DB design
- Phase 6 (was 6): Cart simplified — DB only, login required, no session merge
- Phase 7 (was 7): Image upload in admin uses Cloudinary API
