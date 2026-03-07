using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data.Seeds;

public static class ShopConfigSeed
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ShopConfig>().HasData(
            new ShopConfig { Id = 1, Key = "ShopName", Value = "Nexora", Type = "string" },
            new ShopConfig { Id = 2, Key = "Phone", Value = "1900 8888", Type = "string" },
            new ShopConfig { Id = 3, Key = "Email", Value = "contact@nexora.vn", Type = "string" },
            new ShopConfig { Id = 4, Key = "Address", Value = "268 Lý Thường Kiệt, Q.10, TP. Hồ Chí Minh", Type = "string" },
            new ShopConfig { Id = 5, Key = "Facebook", Value = "https://facebook.com/nexora.vn", Type = "string" },
            new ShopConfig { Id = 6, Key = "WorkingHours", Value = "08:00 - 21:00 (Thứ 2 - Chủ nhật)", Type = "string" },
            new ShopConfig { Id = 7, Key = "AboutUs", Value = "Nexora - Hệ thống bán lẻ công nghệ hàng đầu Việt Nam. Cam kết sản phẩm chính hãng, giá tốt nhất, bảo hành uy tín.", Type = "text" }
        );
    }
}
