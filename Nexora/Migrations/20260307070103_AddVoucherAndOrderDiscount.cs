using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nexora.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherAndOrderDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "numeric(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "integer", nullable: false),
                    MinOrderAmount = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    MaxDiscountAmount = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    UsageLimit = table.Column<int>(type: "integer", nullable: false),
                    UsedCount = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4170));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4173));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 7, 1, 2, 318, DateTimeKind.Utc).AddTicks(4175));

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "Description", "DiscountAmount", "DiscountPercent", "EndDate", "IsActive", "MaxDiscountAmount", "MinOrderAmount", "StartDate", "UsageLimit", "UsedCount" },
                values: new object[,]
                {
                    { 1, "WELCOME10", "Giảm 10% cho đơn đầu tiên", 0m, 10, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), true, 500000m, 1000000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 100, 0 },
                    { 2, "NEXORA50K", "Giảm 50.000đ cho đơn từ 500K", 50000m, 0, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), true, 0m, 500000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 200, 0 },
                    { 3, "FREESHIP", "Giảm 30.000đ phí vận chuyển", 30000m, 0, new DateTime(2026, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc), true, 0m, 300000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 500, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_Code",
                table: "Vouchers",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 15, 39, 23, 419, DateTimeKind.Utc).AddTicks(9729));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 15, 39, 23, 419, DateTimeKind.Utc).AddTicks(9735));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 15, 39, 23, 419, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 15, 39, 23, 419, DateTimeKind.Utc).AddTicks(9738));
        }
    }
}
