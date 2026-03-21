using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ConfigController : Controller
{
    private readonly ApplicationDbContext _db;

    public ConfigController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var configs = await _db.ShopConfigs.OrderBy(c => c.Id).ToListAsync();
        return View(configs);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Dictionary<int, string> values)
    {
        foreach (var (id, value) in values)
        {
            var config = await _db.ShopConfigs.FindAsync(id);
            if (config != null)
            {
                config.Value = value;
            }
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Cap nhat thanh cong!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(string key, string? value, string type = "string")
    {
        if (string.IsNullOrEmpty(key)) return RedirectToAction("Index");

        if (!await _db.ShopConfigs.AnyAsync(c => c.Key == key))
        {
            _db.ShopConfigs.Add(new ShopConfig { Key = key, Value = value, Type = type });
            await _db.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}
