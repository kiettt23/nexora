using Microsoft.AspNetCore.Identity;
using Nexora.Models;

namespace Nexora.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Create roles
        string[] roles = ["Admin", "Staff", "Customer"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Create admin
        if (await userManager.FindByEmailAsync("admin@nexora.vn") == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@nexora.vn",
                Email = "admin@nexora.vn",
                FullName = "Admin Nexora",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Create staff
        if (await userManager.FindByEmailAsync("staff@nexora.vn") == null)
        {
            var staff = new ApplicationUser
            {
                UserName = "staff@nexora.vn",
                Email = "staff@nexora.vn",
                FullName = "Staff Nexora",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(staff, "Staff@123");
            await userManager.AddToRoleAsync(staff, "Staff");
        }

        // Create customer
        if (await userManager.FindByEmailAsync("customer@nexora.vn") == null)
        {
            var customer = new ApplicationUser
            {
                UserName = "customer@nexora.vn",
                Email = "customer@nexora.vn",
                FullName = "Khach Hang",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(customer, "Customer@123");
            await userManager.AddToRoleAsync(customer, "Customer");
        }
    }
}
