using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;

namespace Nexora.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var featuredProducts = await _db.Products
            .Include(p => p.Images)
            .Where(p => p.IsActive && p.IsFeatured)
            .OrderByDescending(p => p.CreatedAt)
            .Take(8)
            .ToListAsync();

        // If no featured products, show newest products
        if (!featuredProducts.Any())
        {
            featuredProducts = await _db.Products
                .Include(p => p.Images)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .ToListAsync();
        }

        // New arrivals (latest products, excluding already featured)
        var featuredIds = featuredProducts.Select(p => p.Id).ToHashSet();
        var newArrivals = await _db.Products
            .Include(p => p.Images)
            .Where(p => p.IsActive && !featuredIds.Contains(p.Id))
            .OrderByDescending(p => p.CreatedAt)
            .Take(8)
            .ToListAsync();

        // Products grouped by category (for tabbed browsing)
        var categories = await _db.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.SortOrder)
            .ToListAsync();

        var activeIds = categories.Select(c => c.Id).ToList();
        var allCategoryProducts = await _db.Products
            .Include(p => p.Images)
            .Where(p => p.IsActive && activeIds.Contains(p.CategoryId))
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        var categoryNameMap = categories.ToDictionary(c => c.Id, c => c.Name);
        var productsByCategory = allCategoryProducts
            .GroupBy(p => p.CategoryId)
            .Where(g => categoryNameMap.ContainsKey(g.Key))
            .ToDictionary(
                g => categoryNameMap[g.Key],
                g => g.Take(4).ToList()
            );

        // Stats
        ViewBag.TotalProducts = await _db.Products.CountAsync(p => p.IsActive);
        ViewBag.TotalBrands = await _db.Products.Where(p => p.IsActive && p.Brand != null).Select(p => p.Brand).Distinct().CountAsync();
        ViewBag.TotalCategories = await _db.Categories.CountAsync(c => c.IsActive);

        ViewBag.FeaturedProducts = featuredProducts;
        ViewBag.NewArrivals = newArrivals;
        ViewBag.ProductsByCategory = productsByCategory;
        ViewBag.Categories = categories;
        return View();
    }

    [Route("/error/404")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = 404;
        return View("NotFound");
    }

    [Route("/error/500")]
    public IActionResult ServerError()
    {
        Response.StatusCode = 500;
        return View("ServerError");
    }
}
