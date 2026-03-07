#!/usr/bin/env python3
"""Generate SQL seed script from products-seed.json for assignment deliverable."""
import json
import os

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
PROJECT_DIR = os.path.dirname(SCRIPT_DIR)
ROOT_DIR = os.path.dirname(PROJECT_DIR)

def esc(s):
    if s is None:
        return None
    return s.replace("'", "''")

def sql_str(s):
    if s is None or s == "":
        return "NULL"
    return f"'{esc(s)}'"

def sql_val(v):
    if v is None:
        return "NULL"
    return str(int(v))

def main():
    with open(os.path.join(PROJECT_DIR, "Data", "products-seed.json"), "r", encoding="utf-8") as f:
        products = json.load(f)

    lines = [
        "-- Nexora Database Seed Script",
        "-- Generated for assignment deliverable",
        "",
        "-- Categories",
    ]

    cats = [
        (1, "Dien thoai", "dien-thoai", "Smartphone cao cap", 1),
        (2, "Laptop", "laptop", "Laptop van phong va gaming", 2),
        (3, "Tablet", "tablet", "May tinh bang", 3),
        (4, "Phu kien", "phu-kien", "Phu kien cong nghe", 4),
    ]
    for cid, name, slug, desc, sort in cats:
        lines.append(
            f'INSERT INTO "Categories" ("Id", "Name", "Slug", "Description", "SortOrder", "IsActive") '
            f"VALUES ({cid}, '{name}', '{slug}', '{desc}', {sort}, true);"
        )

    lines.append("")
    lines.append("-- ShopConfig")
    configs = [
        (1, "ShopName", "Nexora", "string"),
        (2, "Phone", "0123456789", "string"),
        (3, "Email", "contact@nexora.vn", "string"),
        (4, "Address", "TP. Ho Chi Minh, Viet Nam", "string"),
    ]
    for cid, key, val, typ in configs:
        lines.append(
            f'INSERT INTO "ShopConfigs" ("Id", "Key", "Value", "Type") '
            f"VALUES ({cid}, '{key}', '{val}', '{typ}');"
        )

    lines.append("")
    lines.append("-- Products")
    img_id = 1
    img_lines = []

    for p in products:
        pid = p["id"]
        name = sql_str(p["name"])
        slug = sql_str(p["slug"])
        desc = sql_str(p.get("description") or "")
        price = int(p["price"])
        oprice = sql_val(p.get("original_price"))
        brand = sql_str(p.get("brand") or None)
        color = sql_str(p.get("color"))
        storage = sql_str(p.get("storage"))
        ram = sql_str(p.get("ram"))
        screen = sql_str(p.get("screen_size"))
        cpu = sql_str(p.get("cpu"))
        cat = p["category_id"]
        featured = "true" if p.get("is_featured") else "false"

        lines.append(
            f'INSERT INTO "Products" ("Id", "Name", "Slug", "Description", "Price", '
            f'"OriginalPrice", "Brand", "Color", "Storage", "RAM", "ScreenSize", "CPU", '
            f'"CategoryId", "IsActive", "IsFeatured", "CreatedAt") '
            f"VALUES ({pid}, {name}, {slug}, {desc}, {price}, {oprice}, {brand}, {color}, "
            f"{storage}, {ram}, {screen}, {cpu}, {cat}, true, {featured}, NOW());"
        )

        for i, img in enumerate(p.get("images", [])):
            img_url = esc(img)
            is_main = "true" if i == 0 else "false"
            img_lines.append(
                f'INSERT INTO "ProductImages" ("Id", "ProductId", "ImagePath", "SortOrder", "IsMain") '
                f"VALUES ({img_id}, {pid}, '{img_url}', {i}, {is_main});"
            )
            img_id += 1

    lines.append("")
    lines.append("-- ProductImages")
    lines.extend(img_lines)

    lines.append("")
    lines.append("-- Reset sequences")
    lines.append("""SELECT setval('"Categories_Id_seq"', (SELECT MAX("Id") FROM "Categories"));""")
    lines.append("""SELECT setval('"Products_Id_seq"', (SELECT MAX("Id") FROM "Products"));""")
    lines.append("""SELECT setval('"ProductImages_Id_seq"', (SELECT MAX("Id") FROM "ProductImages"));""")
    lines.append("""SELECT setval('"ShopConfigs_Id_seq"', (SELECT MAX("Id") FROM "ShopConfigs"));""")

    out_dir = os.path.join(ROOT_DIR, "Database")
    os.makedirs(out_dir, exist_ok=True)
    out_path = os.path.join(out_dir, "nexora-seed.sql")

    with open(out_path, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))

    print(f"Generated SQL: {len(products)} products, {img_id - 1} images -> {out_path}")

if __name__ == "__main__":
    main()
