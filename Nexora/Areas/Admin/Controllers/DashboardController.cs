using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Staff")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;

    public DashboardController(ApplicationDbContext db)
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

        // Order status distribution
        ViewBag.OrdersByStatus = await _db.Orders
            .GroupBy(o => o.Status)
            .Select(g => new { Status = g.Key.ToString(), Count = g.Count() })
            .ToListAsync();

        // Top selling products
        ViewBag.TopProducts = await _db.OrderDetails
            .GroupBy(od => new { od.ProductId, od.ProductName })
            .Select(g => new { g.Key.ProductName, TotalQty = g.Sum(x => x.Quantity), TotalRevenue = g.Sum(x => x.UnitPrice * x.Quantity) })
            .OrderByDescending(x => x.TotalQty)
            .Take(5)
            .ToListAsync();

        // Monthly revenue (last 6 months)
        var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);
        ViewBag.MonthlyRevenue = await _db.Orders
            .Where(o => o.Status == OrderStatus.Delivered && o.CreatedAt >= sixMonthsAgo)
            .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
            .Select(g => new { g.Key.Year, g.Key.Month, Total = g.Sum(o => o.TotalAmount) })
            .OrderBy(x => x.Year).ThenBy(x => x.Month)
            .ToListAsync();

        // Today stats
        var today = DateTime.UtcNow.Date;
        ViewBag.TodayOrders = await _db.Orders.CountAsync(o => o.CreatedAt >= today);
        ViewBag.TodayRevenue = await _db.Orders
            .Where(o => o.CreatedAt >= today && o.Status == OrderStatus.Delivered)
            .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

        return View();
    }
}
