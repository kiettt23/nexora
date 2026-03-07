using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Controllers;

[Authorize(Roles = "Admin,Staff")]
public class AdminCategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public AdminCategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _db.Categories
            .OrderBy(c => c.SortOrder)
            .ToListAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create() => View(new Category());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category model)
    {
        if (!ModelState.IsValid) return View(model);

        model.Slug = model.Name.ToLowerInvariant().Replace(" ", "-");
        _db.Categories.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category model)
    {
        if (!ModelState.IsValid) return View(model);

        var category = await _db.Categories.FindAsync(id);
        if (category == null) return NotFound();

        category.Name = model.Name;
        category.Slug = model.Name.ToLowerInvariant().Replace(" ", "-");
        category.Description = model.Description;
        category.ImagePath = model.ImagePath;
        category.SortOrder = model.SortOrder;
        category.IsActive = model.IsActive;

        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
