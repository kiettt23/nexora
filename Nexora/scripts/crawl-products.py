"""
Crawl tech products from Thegioididong.com
Exports JSON for EF Core seeding
"""
import sys
import io
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8', errors='replace')

import requests
from bs4 import BeautifulSoup
import json
import time
import re
import os

HEADERS = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36",
    "Accept-Language": "vi-VN,vi;q=0.9",
}

CATEGORIES = [
    {"name": "Dien thoai", "id": 1, "slug": "dien-thoai", "url": "https://www.thegioididong.com/dtdd", "limit": 40},
    {"name": "Laptop", "id": 2, "slug": "laptop", "url": "https://www.thegioididong.com/laptop", "limit": 30},
    {"name": "Tablet", "id": 3, "slug": "tablet", "url": "https://www.thegioididong.com/may-tinh-bang", "limit": 15},
    {"name": "Phu kien", "id": 4, "slug": "phu-kien", "url": "https://www.thegioididong.com/phu-kien-dien-thoai", "limit": 20},
]

def slugify(text):
    """Convert Vietnamese text to slug"""
    text = text.lower().strip()
    # Vietnamese diacritics mapping
    replacements = {
        'à': 'a', 'á': 'a', 'ả': 'a', 'ã': 'a', 'ạ': 'a',
        'ă': 'a', 'ằ': 'a', 'ắ': 'a', 'ẳ': 'a', 'ẵ': 'a', 'ặ': 'a',
        'â': 'a', 'ầ': 'a', 'ấ': 'a', 'ẩ': 'a', 'ẫ': 'a', 'ậ': 'a',
        'đ': 'd',
        'è': 'e', 'é': 'e', 'ẻ': 'e', 'ẽ': 'e', 'ẹ': 'e',
        'ê': 'e', 'ề': 'e', 'ế': 'e', 'ể': 'e', 'ễ': 'e', 'ệ': 'e',
        'ì': 'i', 'í': 'i', 'ỉ': 'i', 'ĩ': 'i', 'ị': 'i',
        'ò': 'o', 'ó': 'o', 'ỏ': 'o', 'õ': 'o', 'ọ': 'o',
        'ô': 'o', 'ồ': 'o', 'ố': 'o', 'ổ': 'o', 'ỗ': 'o', 'ộ': 'o',
        'ơ': 'o', 'ờ': 'o', 'ớ': 'o', 'ở': 'o', 'ỡ': 'o', 'ợ': 'o',
        'ù': 'u', 'ú': 'u', 'ủ': 'u', 'ũ': 'u', 'ụ': 'u',
        'ư': 'u', 'ừ': 'u', 'ứ': 'u', 'ử': 'u', 'ữ': 'u', 'ự': 'u',
        'ỳ': 'y', 'ý': 'y', 'ỷ': 'y', 'ỹ': 'y', 'ỵ': 'y',
    }
    for vn, en in replacements.items():
        text = text.replace(vn, en)
    text = re.sub(r'[^a-z0-9\s-]', '', text)
    text = re.sub(r'[\s-]+', '-', text)
    return text.strip('-')

def parse_price(text):
    """Extract price number from Vietnamese price text"""
    if not text:
        return None
    nums = re.findall(r'[\d.]+', text.replace('.', ''))
    if nums:
        try:
            return int(nums[0])
        except ValueError:
            return None
    return None

def crawl_product_list(url, limit):
    """Get product links from category page"""
    print(f"  Crawling list: {url}")
    try:
        resp = requests.get(url, headers=HEADERS, timeout=15)
        resp.raise_for_status()
    except Exception as e:
        print(f"  ERROR fetching list: {e}")
        return []

    soup = BeautifulSoup(resp.text, "html.parser")
    products = []

    # Try multiple selectors for product cards
    cards = soup.select("li.item, .item.__cate_44, .item-product, ul.listproduct > li")
    if not cards:
        cards = soup.select(".categoryPage .item, .listproduct .item")

    print(f"  Found {len(cards)} product cards")

    for card in cards[:limit]:
        link = card.select_one("a[href]")
        if not link:
            continue

        href = link.get("href", "")
        if not href.startswith("http"):
            href = "https://www.thegioididong.com" + href

        name_el = card.select_one("h3, .item-name, .name")
        name = name_el.get_text(strip=True) if name_el else ""

        price_el = card.select_one(".price, .price-new, strong.price")
        price_text = price_el.get_text(strip=True) if price_el else ""
        price = parse_price(price_text)

        old_price_el = card.select_one(".price-old, .price-sale, del")
        old_price = parse_price(old_price_el.get_text(strip=True)) if old_price_el else None

        img_el = card.select_one("img[data-src], img[src]")
        img_url = ""
        if img_el:
            img_url = img_el.get("data-src") or img_el.get("src") or ""
            if img_url and not img_url.startswith("http"):
                img_url = "https:" + img_url

        if name and price and price > 0:
            products.append({
                "name": name,
                "url": href,
                "price": price,
                "original_price": old_price,
                "image": img_url,
            })

    return products

def crawl_product_detail(url, category_slug):
    """Get detailed specs from product page"""
    try:
        time.sleep(1.5)
        resp = requests.get(url, headers=HEADERS, timeout=15)
        resp.raise_for_status()
    except Exception as e:
        print(f"    ERROR fetching detail: {e}")
        return {}

    soup = BeautifulSoup(resp.text, "html.parser")
    specs = {}

    # Extract brand
    brand_el = soup.select_one(".brand-name a, .brand a, a[href*='hang-'] b")
    if brand_el:
        specs["brand"] = brand_el.get_text(strip=True)

    # Extract specs from parameter table
    param_items = soup.select(".parameter-item, .specifi li, .box-specifi li, ul.parameter li")
    for item in param_items:
        label_el = item.select_one(".parameter-name, span:first-child, .ctLeft, p:first-child")
        value_el = item.select_one(".parameter-value, span:last-child, .ctRight, p:last-child")
        if label_el and value_el:
            label = label_el.get_text(strip=True).lower()
            value = value_el.get_text(strip=True)

            if "ram" in label:
                specs["ram"] = value
            elif "rom" in label or "lưu trữ" in label or "ổ cứng" in label or "bộ nhớ trong" in label:
                specs["storage"] = value
            elif "màn hình" in label or "kích thước" in label:
                specs["screen_size"] = value
            elif "cpu" in label or "chip" in label or "vi xử lý" in label:
                specs["cpu"] = value
            elif "màu" in label:
                specs["color"] = value

    # Extra images
    images = []
    gallery = soup.select(".slide-show img, .gallery img, .owl-item img, .list-img-detail img")
    for img in gallery[:5]:
        src = img.get("data-src") or img.get("src") or ""
        if src and not src.startswith("http"):
            src = "https:" + src
        if src and "icon" not in src.lower() and src not in images:
            images.append(src)
    specs["extra_images"] = images

    # Description
    desc_el = soup.select_one(".article-content, .box-des, .des")
    if desc_el:
        specs["description"] = desc_el.get_text(strip=True)[:500]

    return specs


def main():
    all_products = []
    product_id = 1

    for cat in CATEGORIES:
        print(f"\n=== {cat['name']} (limit {cat['limit']}) ===")

        items = crawl_product_list(cat["url"], cat["limit"])
        print(f"  Got {len(items)} products from list")

        for i, item in enumerate(items):
            print(f"  [{i+1}/{len(items)}] {item['name'][:50]}...")

            # Get detail specs
            detail = crawl_product_detail(item["url"], cat["slug"])

            # Build product object
            slug = slugify(item["name"])
            # Ensure unique slug
            existing_slugs = [p["slug"] for p in all_products]
            if slug in existing_slugs:
                slug = f"{slug}-{product_id}"

            images = [item["image"]] if item["image"] else []
            images.extend(detail.get("extra_images", []))
            # Deduplicate
            seen = set()
            unique_images = []
            for img in images:
                if img and img not in seen:
                    seen.add(img)
                    unique_images.append(img)

            product = {
                "id": product_id,
                "name": item["name"],
                "slug": slug,
                "description": detail.get("description", ""),
                "price": item["price"],
                "original_price": item.get("original_price"),
                "brand": detail.get("brand", ""),
                "color": detail.get("color"),
                "storage": detail.get("storage"),
                "ram": detail.get("ram"),
                "screen_size": detail.get("screen_size"),
                "cpu": detail.get("cpu"),
                "category_id": cat["id"],
                "is_active": True,
                "is_featured": i < 5,  # First 5 in each category are featured
                "images": unique_images[:5],
            }

            all_products.append(product)
            product_id += 1

    # Save JSON
    output_path = os.path.join(os.path.dirname(__file__), "..", "Data", "products-seed.json")
    with open(output_path, "w", encoding="utf-8") as f:
        json.dump(all_products, f, ensure_ascii=False, indent=2)

    print(f"\n=== Done! {len(all_products)} products saved to {output_path} ===")

    # Stats
    for cat in CATEGORIES:
        count = len([p for p in all_products if p["category_id"] == cat["id"]])
        print(f"  {cat['name']}: {count}")


if __name__ == "__main__":
    main()
