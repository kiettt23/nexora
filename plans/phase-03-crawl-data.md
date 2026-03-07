# Phase 8 — Crawl Product Data

## Priority: MEDIUM
## Status: Done

## Overview
Crawl ~105 tech products from Thegioididong.com. Export as seed data (SQL or JSON).

## Target
| Category | Quantity | URL Pattern |
|---|---|---|
| Phones | ~40 | thegioididong.com/dtdd |
| Laptops | ~30 | thegioididong.com/laptop |
| Tablets | ~15 | thegioididong.com/may-tinh-bang |
| Accessories | ~20 | thegioididong.com/phu-kien |

## Data Per Product
- Name, brand, price, original price
- Images (download to local, multiple per product)
- Specs: RAM, storage, screen size, CPU, color
- Description (short)
- Category mapping

## Implementation Steps
1. Write Python crawl script (requests + BeautifulSoup)
2. Add delay between requests (1-2s) + browser headers
3. Crawl product list pages → get product URLs
4. Crawl each product page → extract data + download images
5. Save images to `wwwroot/images/products/{category}/{slug}/`
6. Export data as:
   - JSON file (for EF Core seed)
   - SQL insert script (for assignment deliverable)
7. Create EF Core seed from JSON
8. Generate slug from product name

## Todo
- [ ] Write crawl script (Python)
- [ ] Crawl phones (~40)
- [ ] Crawl laptops (~30)
- [ ] Crawl tablets (~15)
- [ ] Crawl accessories (~20)
- [ ] Download images to wwwroot
- [ ] Export JSON + SQL
- [ ] Create EF Core seed data

## Success Criteria
- ~105 products with images crawled
- Data clean and consistent
- Images organized in folders
- Seed data loads successfully
- SQL script works standalone
