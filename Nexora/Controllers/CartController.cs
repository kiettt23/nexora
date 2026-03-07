using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var cart = await GetOrCreateCartAsync();
        var cartDetails = await _db.CartDetails
            .Include(cd => cd.Product)
                .ThenInclude(p => p.Images)
            .Where(cd => cd.CartId == cart.Id)
            .ToListAsync();

        return View(cartDetails);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, int quantity = 1)
    {
        if (quantity <= 0) quantity = 1;

        var product = await _db.Products.FindAsync(productId);
        if (product == null || !product.IsActive)
            return NotFound();

        var cart = await GetOrCreateCartAsync();
        var existing = await _db.CartDetails
            .FirstOrDefaultAsync(cd => cd.CartId == cart.Id && cd.ProductId == productId);

        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _db.CartDetails.Add(new CartDetail
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price
            });
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var count = await _db.CartDetails.Where(cd => cd.CartId == cart.Id).SumAsync(cd => cd.Quantity);

        if (Request.Headers["HX-Request"] == "true")
            return Content(count.ToString());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int cartDetailId, int quantity)
    {
        var userId = _userManager.GetUserId(User)!;
        var detail = await _db.CartDetails
            .Include(cd => cd.Cart)
            .FirstOrDefaultAsync(cd => cd.Id == cartDetailId && cd.Cart.UserId == userId);

        if (detail == null) return NotFound();

        if (quantity <= 0)
        {
            _db.CartDetails.Remove(detail);
        }
        else
        {
            detail.Quantity = quantity;
        }

        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int cartDetailId)
    {
        var userId = _userManager.GetUserId(User)!;
        var detail = await _db.CartDetails
            .Include(cd => cd.Cart)
            .FirstOrDefaultAsync(cd => cd.Id == cartDetailId && cd.Cart.UserId == userId);

        if (detail != null)
        {
            _db.CartDetails.Remove(detail);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Count()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Content("0");

        var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null) return Content("0");

        var count = await _db.CartDetails
            .Where(cd => cd.CartId == cart.Id)
            .SumAsync(cd => cd.Quantity);

        return Content(count.ToString());
    }

    private async Task<Cart> GetOrCreateCartAsync()
    {
        var userId = _userManager.GetUserId(User)!;
        var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
        }

        return cart;
    }
}
