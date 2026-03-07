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

        // Seed users
        await SeedUserAsync(userManager, "admin@nexora.vn", "Admin Nexora", "Admin@123", "Admin");
        await SeedUserAsync(userManager, "staff@nexora.vn", "Nguyễn Văn Bình", "Staff@123", "Staff");
        await SeedUserAsync(userManager, "staff2@nexora.vn", "Trần Thị Cẩm", "Staff@123", "Staff");
        await SeedUserAsync(userManager, "customer@nexora.vn", "Lê Hoàng Dũng", "Customer@123", "Customer");
        await SeedUserAsync(userManager, "ngoclan@gmail.com", "Phạm Ngọc Lan", "Customer@123", "Customer");
        await SeedUserAsync(userManager, "minhtuan@gmail.com", "Võ Minh Tuấn", "Customer@123", "Customer");
        await SeedUserAsync(userManager, "thuhang@gmail.com", "Đặng Thu Hằng", "Customer@123", "Customer");
        await SeedUserAsync(userManager, "quocviet@gmail.com", "Huỳnh Quốc Việt", "Customer@123", "Customer");
    }

    private static async Task SeedUserAsync(
        UserManager<ApplicationUser> userManager,
        string email, string fullName, string password, string role)
    {
        if (await userManager.FindByEmailAsync(email) != null)
            return;

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, role);
    }
}
