using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;

namespace Nexora.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;
    private const int PageSize = 12;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(
        string? category, string? brand, string? search,
        decimal? minPrice, decimal? maxPrice,
        string? sort, int page = 1)
    {
        var query = _db.Products
            .Include(p => p.Images)
            .Include(p => p.Category)
            .Where(p => p.IsActive);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.Category.Slug == category);

        if (!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand == brand);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        query = sort switch
        {
            "price-asc" => query.OrderBy(p => p.Price),
            "price-desc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        var products = await query
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var categories = await _db.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.SortOrder)
            .ToListAsync();

        var brands = await _db.Products
            .Where(p => p.IsActive && p.Brand != null)
            .Select(p => p.Brand!)
            .Distinct()
            .OrderBy(b => b)
            .ToListAsync();

        ViewBag.Categories = categories;
        ViewBag.Brands = brands;
        ViewBag.CurrentCategory = category;
        ViewBag.CurrentBrand = brand;
        ViewBag.CurrentSearch = search;
        ViewBag.CurrentSort = sort;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalItems = totalItems;

        if (Request.Headers["HX-Request"] == "true")
            return PartialView("_ProductGrid", products);

        return View(products);
    }

    public async Task<IActionResult> Detail(string slug)
    {
        var product = await _db.Products
            .Include(p => p.Images.OrderBy(i => i.SortOrder))
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);

        if (product == null)
            return NotFound();

        var relatedProducts = await _db.Products
            .Include(p => p.Images)
            .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id && p.IsActive)
            .Take(4)
            .ToListAsync();

        ViewBag.RelatedProducts = relatedProducts;
        return View(product);
    }
}
