using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;
using Nexora.Models.ViewModels;
using Nexora.Services;

namespace Nexora.Controllers;

[Authorize(Roles = "Admin,Staff")]
public class AdminProductController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly CloudinaryService _cloudinary;

    public AdminProductController(ApplicationDbContext db, CloudinaryService cloudinary)
    {
        _db = db;
        _cloudinary = cloudinary;
    }

    public async Task<IActionResult> Index(string? search, int? categoryId, int page = 1)
    {
        var query = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        var totalItems = await query.CountAsync();
        var products = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * 20)
            .Take(20)
            .ToListAsync();

        ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalItems / 20.0);

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(
            await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name");
        return View(new ProductFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(
                await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name");
            return View(model);
        }

        var slug = GenerateSlug(model.Name);
        if (await _db.Products.AnyAsync(p => p.Slug == slug))
            slug = $"{slug}-{DateTime.UtcNow.Ticks % 10000}";

        var product = new Product
        {
            Name = model.Name,
            Slug = slug,
            Description = model.Description,
            Price = model.Price,
            OriginalPrice = model.OriginalPrice,
            Brand = model.Brand,
            Color = model.Color,
            Storage = model.Storage,
            RAM = model.RAM,
            ScreenSize = model.ScreenSize,
            CPU = model.CPU,
            CategoryId = model.CategoryId,
            IsActive = true,
            IsFeatured = model.IsFeatured
        };

        var imageUrls = await CollectImageUrls(model);
        for (int i = 0; i < imageUrls.Count; i++)
        {
            product.Images.Add(new ProductImage
            {
                ImagePath = imageUrls[i],
                SortOrder = i,
                IsMain = i == 0
            });
        }

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Tạo sản phẩm thành công!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _db.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        ViewBag.Categories = new SelectList(
            await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name", product.CategoryId);

        var model = new ProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            OriginalPrice = product.OriginalPrice,
            Brand = product.Brand,
            Color = product.Color,
            Storage = product.Storage,
            RAM = product.RAM,
            ScreenSize = product.ScreenSize,
            CPU = product.CPU,
            CategoryId = product.CategoryId,
            IsFeatured = product.IsFeatured,
            ImageUrls = string.Join(",", product.Images.OrderBy(i => i.SortOrder).Select(i => i.ImagePath))
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(
                await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name", model.CategoryId);
            return View(model);
        }

        var product = await _db.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        var slug = GenerateSlug(model.Name);
        if (await _db.Products.AnyAsync(p => p.Slug == slug && p.Id != id))
            slug = $"{slug}-{DateTime.UtcNow.Ticks % 10000}";

        product.Name = model.Name;
        product.Slug = slug;
        product.Description = model.Description;
        product.Price = model.Price;
        product.OriginalPrice = model.OriginalPrice;
        product.Brand = model.Brand;
        product.Color = model.Color;
        product.Storage = model.Storage;
        product.RAM = model.RAM;
        product.ScreenSize = model.ScreenSize;
        product.CPU = model.CPU;
        product.CategoryId = model.CategoryId;
        product.IsFeatured = model.IsFeatured;
        product.UpdatedAt = DateTime.UtcNow;

        // Update images
        _db.ProductImages.RemoveRange(product.Images);
        var imageUrls = await CollectImageUrls(model);
        for (int i = 0; i < imageUrls.Count; i++)
        {
            product.Images.Add(new ProductImage
            {
                ImagePath = imageUrls[i],
                SortOrder = i,
                IsMain = i == 0
            });
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Cập nhật sản phẩm thành công!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.IsActive = !product.IsActive;
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private async Task<List<string>> CollectImageUrls(ProductFormViewModel model)
    {
        var urls = new List<string>();

        // Upload new files to Cloudinary
        if (model.ImageFiles != null)
        {
            foreach (var file in model.ImageFiles)
            {
                if (file.Length == 0) continue;
                var url = await _cloudinary.UploadAsync(file);
                if (url != null) urls.Add(url);
            }
        }

        // Keep existing URLs
        if (!string.IsNullOrEmpty(model.ImageUrls))
        {
            var existingUrls = model.ImageUrls
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            urls.AddRange(existingUrls);
        }

        return urls;
    }

    private static string GenerateSlug(string name)
    {
        var slug = name.ToLowerInvariant()
            .Replace("đ", "d");

        // Remove diacritics
        var normalized = slug.Normalize(System.Text.NormalizationForm.FormD);
        var sb = new System.Text.StringBuilder();
        foreach (var c in normalized)
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        slug = sb.ToString();

        // Replace non-alphanumeric with hyphen
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9]+", "-");

        return slug.Trim('-');
    }
}
