using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data.Seeds;

public static class VoucherSeed
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<Voucher>().HasData(
            // Voucher phan tram - co max discount
            new Voucher
            {
                Id = 1, Code = "WELCOME10", Description = "Giảm 10% cho đơn đầu tiên",
                DiscountPercent = 10, MaxDiscountAmount = 500000, MinOrderAmount = 1000000,
                UsageLimit = 100,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher giam co dinh
            new Voucher
            {
                Id = 2, Code = "NEXORA50K", Description = "Giảm 50.000đ cho đơn từ 500K",
                DiscountAmount = 50000, MinOrderAmount = 500000,
                UsageLimit = 200,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher freeship
            new Voucher
            {
                Id = 3, Code = "FREESHIP", Description = "Giảm 30.000đ phí vận chuyển",
                DiscountAmount = 30000, MinOrderAmount = 300000,
                UsageLimit = 500,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher da het han (edge case: expired)
            new Voucher
            {
                Id = 4, Code = "TETAM2025", Description = "Giảm 15% mừng Tết Âm lịch 2025",
                DiscountPercent = 15, MaxDiscountAmount = 1000000, MinOrderAmount = 2000000,
                UsageLimit = 50, UsedCount = 42,
                StartDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2025, 2, 15, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher da het luot (edge case: maxed usage)
            new Voucher
            {
                Id = 5, Code = "FLASH100", Description = "Flash sale giảm 100K",
                DiscountAmount = 100000, MinOrderAmount = 500000,
                UsageLimit = 10, UsedCount = 10,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher giam lon, min cao (edge case: high threshold)
            new Voucher
            {
                Id = 6, Code = "BIGDEAL20", Description = "Giảm 20% cho đơn từ 20 triệu",
                DiscountPercent = 20, MaxDiscountAmount = 5000000, MinOrderAmount = 20000000,
                UsageLimit = 20,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher khong yeu cau min (edge case: no minimum)
            new Voucher
            {
                Id = 7, Code = "GIFT20K", Description = "Tặng 20K cho mọi đơn hàng",
                DiscountAmount = 20000, MinOrderAmount = 0,
                UsageLimit = 1000,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            // Voucher bi vo hieu hoa (edge case: disabled)
            new Voucher
            {
                Id = 8, Code = "DISABLED", Description = "Voucher đã bị vô hiệu hóa",
                DiscountAmount = 500000, MinOrderAmount = 0,
                UsageLimit = 100, IsActive = false,
                StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            }
        );
    }
}
