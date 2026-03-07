using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data.Seeds;

public static class CategorySeed
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Điện thoại", Slug = "dien-thoai", Description = "Smartphone cao cấp từ các thương hiệu hàng đầu", SortOrder = 1 },
            new Category { Id = 2, Name = "Laptop", Slug = "laptop", Description = "Laptop văn phòng, học tập và gaming", SortOrder = 2 },
            new Category { Id = 3, Name = "Tablet", Slug = "tablet", Description = "Máy tính bảng đa năng", SortOrder = 3 },
            new Category { Id = 4, Name = "Phụ kiện", Slug = "phu-kien", Description = "Phụ kiện công nghệ: sạc, cáp, loa, tai nghe,...", SortOrder = 4 },
            new Category { Id = 5, Name = "Đồng hồ thông minh", Slug = "dong-ho-thong-minh", Description = "Smartwatch và thiết bị đeo thông minh", SortOrder = 5 },
            new Category { Id = 6, Name = "Âm thanh", Slug = "am-thanh", Description = "Loa, tai nghe, thiết bị âm thanh chất lượng cao", SortOrder = 6 }
        );
    }
}
