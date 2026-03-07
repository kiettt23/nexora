using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;
using Nexora.Models.ViewModels;

namespace Nexora.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var userId = _userManager.GetUserId(User)!;
        var user = await _userManager.FindByIdAsync(userId);
        var cart = await _db.Carts
            .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                    .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.CartDetails.Any())
            return RedirectToAction("Index", "Cart");

        ViewBag.CartDetails = cart.CartDetails.ToList();
        ViewBag.Total = cart.CartDetails.Sum(cd => cd.UnitPrice * cd.Quantity);

        var model = new CheckoutViewModel
        {
            FullName = user?.FullName ?? "",
            Phone = user?.Phone ?? "",
            Address = user?.Address ?? ""
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        var userId = _userManager.GetUserId(User)!;
        var cart = await _db.Carts
            .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.CartDetails.Any())
            return RedirectToAction("Index", "Cart");

        if (!ModelState.IsValid)
        {
            ViewBag.CartDetails = cart.CartDetails.ToList();
            ViewBag.Total = cart.CartDetails.Sum(cd => cd.UnitPrice * cd.Quantity);
            return View(model);
        }

        var subtotal = cart.CartDetails.Sum(cd => cd.UnitPrice * cd.Quantity);
        decimal discountAmount = 0;
        string? appliedVoucher = null;

        if (!string.IsNullOrWhiteSpace(model.VoucherCode))
        {
            var voucher = await _db.Vouchers.FirstOrDefaultAsync(v => v.Code == model.VoucherCode.Trim().ToUpper());
            if (voucher == null || !voucher.IsValid)
            {
                ModelState.AddModelError("VoucherCode", "Mã giảm giá không hợp lệ hoặc đã hết hạn.");
                ViewBag.CartDetails = cart.CartDetails.ToList();
                ViewBag.Total = subtotal;
                return View(model);
            }
            if (subtotal < voucher.MinOrderAmount)
            {
                ModelState.AddModelError("VoucherCode", $"Đơn hàng tối thiểu {voucher.MinOrderAmount:N0}đ để sử dụng mã này.");
                ViewBag.CartDetails = cart.CartDetails.ToList();
                ViewBag.Total = subtotal;
                return View(model);
            }

            if (voucher.DiscountPercent > 0)
                discountAmount = subtotal * voucher.DiscountPercent / 100;
            else
                discountAmount = voucher.DiscountAmount;

            if (voucher.MaxDiscountAmount > 0 && discountAmount > voucher.MaxDiscountAmount)
                discountAmount = voucher.MaxDiscountAmount;

            voucher.UsedCount++;
            appliedVoucher = voucher.Code;
        }

        var orderCode = $"NX-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

        var order = new Order
        {
            UserId = userId,
            OrderCode = orderCode,
            FullName = model.FullName,
            Phone = model.Phone,
            Address = model.Address,
            Note = model.Note,
            TotalAmount = subtotal - discountAmount,
            DiscountAmount = discountAmount,
            VoucherCode = appliedVoucher,
            Status = OrderStatus.Pending,
            PaymentMethod = "COD",
            OrderDetails = cart.CartDetails.Select(cd => new OrderDetail
            {
                ProductId = cd.ProductId,
                ProductName = cd.Product.Name,
                Quantity = cd.Quantity,
                UnitPrice = cd.UnitPrice
            }).ToList()
        };

        _db.Orders.Add(order);
        _db.CartDetails.RemoveRange(cart.CartDetails);
        await _db.SaveChangesAsync();

        return RedirectToAction("Success", new { orderCode });
    }

    [HttpGet]
    public async Task<IActionResult> Success(string orderCode)
    {
        var userId = _userManager.GetUserId(User)!;
        var order = await _db.Orders
            .FirstOrDefaultAsync(o => o.OrderCode == orderCode && o.UserId == userId);

        if (order == null) return NotFound();

        return View(order);
    }

    [HttpGet]
    public async Task<IActionResult> MyOrders()
    {
        var userId = _userManager.GetUserId(User)!;
        var orders = await _db.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var order = await _db.Orders
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

        if (order == null) return NotFound();

        return View(order);
    }
}
