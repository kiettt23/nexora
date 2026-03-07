using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminUserController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index(string? search)
    {
        var users = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(search))
            users = users.Where(u => u.FullName.Contains(search) || u.Email!.Contains(search));

        var userList = await users.OrderByDescending(u => u.CreatedAt).ToListAsync();

        var usersWithRoles = new List<(ApplicationUser User, IList<string> Roles)>();
        foreach (var user in userList)
        {
            var roles = await _userManager.GetRolesAsync(user);
            usersWithRoles.Add((user, roles));
        }

        ViewBag.Search = search;
        return View(usersWithRoles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.IsActive = !user.IsActive;
        await _userManager.UpdateAsync(user);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeRole(string id, string role)
    {
        string[] allowedRoles = ["Admin", "Staff", "Customer"];
        if (!allowedRoles.Contains(role)) return BadRequest();

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, role);

        return RedirectToAction("Index");
    }
}
