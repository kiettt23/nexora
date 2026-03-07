using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nexora.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 15, 4, 444, DateTimeKind.Utc).AddTicks(2416), "Smartphone cao cấp từ các thương hiệu hàng đầu", "Điện thoại" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 15, 4, 444, DateTimeKind.Utc).AddTicks(2425), "Laptop văn phòng, học tập và gaming" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 15, 4, 444, DateTimeKind.Utc).AddTicks(2428), "Máy tính bảng đa năng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 15, 4, 444, DateTimeKind.Utc).AddTicks(2430), "Phụ kiện công nghệ: sạc, cáp, loa, tai nghe,...", "Phụ kiện" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImagePath", "IsActive", "Name", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { 5, new DateTime(2026, 3, 7, 7, 15, 4, 444, DateTimeKind.Utc).AddTicks(2431), "Smartwatch và thiết bị đeo thông minh", null, true, "Đồng hồ thông minh", "dong-ho-thong-minh", 5 },
                    { 6, new DateTime(2026, 3, 7, 7, 15, 4, 444, DateTimeKind.Utc).AddTicks(2433), "Loa, tai nghe, thiết bị âm thanh chất lượng cao", null, true, "Âm thanh", "am-thanh", 6 }
                });

            migrationBuilder.UpdateData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Value",
                value: "1900 8888");

            migrationBuilder.UpdateData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Value",
                value: "268 Lý Thường Kiệt, Q.10, TP. Hồ Chí Minh");

            migrationBuilder.InsertData(
                table: "ShopConfigs",
                columns: new[] { "Id", "Key", "Type", "Value" },
                values: new object[,]
                {
                    { 5, "Facebook", "string", "https://facebook.com/nexora.vn" },
                    { 6, "WorkingHours", "string", "08:00 - 21:00 (Thứ 2 - Chủ nhật)" },
                    { 7, "AboutUs", "text", "Nexora - Hệ thống bán lẻ công nghệ hàng đầu Việt Nam. Cam kết sản phẩm chính hãng, giá tốt nhất, bảo hành uy tín." }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "Description", "DiscountAmount", "DiscountPercent", "EndDate", "IsActive", "MaxDiscountAmount", "MinOrderAmount", "StartDate", "UsageLimit", "UsedCount" },
                values: new object[,]
                {
                    { 4, "TETAM2025", "Giảm 15% mừng Tết Âm lịch 2025", 0m, 15, new DateTime(2025, 2, 15, 23, 59, 59, 0, DateTimeKind.Utc), true, 1000000m, 2000000m, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 50, 42 },
                    { 5, "FLASH100", "Flash sale giảm 100K", 100000m, 0, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), true, 0m, 500000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 10 },
                    { 6, "BIGDEAL20", "Giảm 20% cho đơn từ 20 triệu", 0m, 20, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), true, 5000000m, 20000000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 0 },
                    { 7, "GIFT20K", "Tặng 20K cho mọi đơn hàng", 20000m, 0, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), true, 0m, 0m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1000, 0 },
                    { 8, "DISABLED", "Voucher đã bị vô hiệu hóa", 500000m, 0, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), false, 0m, 0m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 100, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4161), "Smartphone cao cap", "Dien thoai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4170), "Laptop van phong va gaming" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4173), "May tinh bang" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4175), "Phu kien cong nghe", "Phu kien" });

            migrationBuilder.UpdateData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Value",
                value: "0123456789");

            migrationBuilder.UpdateData(
                table: "ShopConfigs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Value",
                value: "TP. Ho Chi Minh, Viet Nam");
        }
    }
}
