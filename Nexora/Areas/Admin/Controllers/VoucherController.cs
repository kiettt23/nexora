using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class VoucherController : Controller
{
    private readonly ApplicationDbContext _db;

    public VoucherController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var vouchers = await _db.Vouchers
            .OrderByDescending(v => v.EndDate)
            .ToListAsync();
        return View(vouchers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new Voucher
        {
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            UsageLimit = 100,
            IsActive = true
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Voucher model)
    {
        if (!ModelState.IsValid) return View(model);

        model.Code = model.Code.Trim().ToUpper();

        if (await _db.Vouchers.AnyAsync(v => v.Code == model.Code))
        {
            ModelState.AddModelError("Code", "Mã voucher đã tồn tại.");
            return View(model);
        }

        model.StartDate = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Utc);
        model.EndDate = DateTime.SpecifyKind(model.EndDate, DateTimeKind.Utc);

        _db.Vouchers.Add(model);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Tạo voucher thành công!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var voucher = await _db.Vouchers.FindAsync(id);
        if (voucher == null) return NotFound();
        return View(voucher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Voucher model)
    {
        if (!ModelState.IsValid) return View(model);

        var voucher = await _db.Vouchers.FindAsync(id);
        if (voucher == null) return NotFound();

        model.Code = model.Code.Trim().ToUpper();

        if (await _db.Vouchers.AnyAsync(v => v.Code == model.Code && v.Id != id))
        {
            ModelState.AddModelError("Code", "Mã voucher đã tồn tại.");
            return View(model);
        }

        voucher.Code = model.Code;
        voucher.Description = model.Description;
        voucher.DiscountAmount = model.DiscountAmount;
        voucher.DiscountPercent = model.DiscountPercent;
        voucher.MinOrderAmount = model.MinOrderAmount;
        voucher.MaxDiscountAmount = model.MaxDiscountAmount;
        voucher.UsageLimit = model.UsageLimit;
        voucher.StartDate = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Utc);
        voucher.EndDate = DateTime.SpecifyKind(model.EndDate, DateTimeKind.Utc);
        voucher.IsActive = model.IsActive;

        await _db.SaveChangesAsync();

        TempData["Success"] = "Cập nhật voucher thành công!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var voucher = await _db.Vouchers.FindAsync(id);
        if (voucher == null) return NotFound();

        _db.Vouchers.Remove(voucher);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Đã xóa voucher.";
        return RedirectToAction("Index");
    }
}
