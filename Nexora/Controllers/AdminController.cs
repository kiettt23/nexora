using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Controllers;

[Authorize(Roles = "Admin,Staff")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;

    public AdminController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalProducts = await _db.Products.CountAsync(p => p.IsActive);
        ViewBag.TotalOrders = await _db.Orders.CountAsync();
        ViewBag.TotalUsers = await _db.Users.CountAsync();
        ViewBag.TotalRevenue = await _db.Orders
            .Where(o => o.Status == OrderStatus.Delivered)
            .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;
        ViewBag.PendingOrders = await _db.Orders.CountAsync(o => o.Status == OrderStatus.Pending);
        ViewBag.RecentOrders = await _db.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.CreatedAt)
            .Take(5)
            .ToListAsync();

        return View();
    }
}
