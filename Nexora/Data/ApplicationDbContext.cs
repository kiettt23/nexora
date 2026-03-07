using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartDetail> CartDetails => Set<CartDetail>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<ShopConfig> ShopConfigs => Set<ShopConfig>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Category
        builder.Entity<Category>(e =>
        {
            e.HasIndex(c => c.Slug).IsUnique();
        });

        // Product
        builder.Entity<Product>(e =>
        {
            e.HasIndex(p => p.Slug).IsUnique();
            e.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ProductImage
        builder.Entity<ProductImage>(e =>
        {
            e.HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Cart — one per user
        builder.Entity<Cart>(e =>
        {
            e.HasIndex(c => c.UserId).IsUnique();
            e.HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // CartDetail
        builder.Entity<CartDetail>(e =>
        {
            e.HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(cd => cd.Product)
                .WithMany()
                .HasForeignKey(cd => cd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Order
        builder.Entity<Order>(e =>
        {
            e.HasIndex(o => o.OrderCode).IsUnique();
            e.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // OrderDetail
        builder.Entity<OrderDetail>(e =>
        {
            e.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ShopConfig
        builder.Entity<ShopConfig>(e =>
        {
            e.HasIndex(sc => sc.Key).IsUnique();
        });

        // Voucher
        builder.Entity<Voucher>(e =>
        {
            e.HasIndex(v => v.Code).IsUnique();
        });

        // Seed categories
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Dien thoai", Slug = "dien-thoai", Description = "Smartphone cao cap", SortOrder = 1 },
            new Category { Id = 2, Name = "Laptop", Slug = "laptop", Description = "Laptop van phong va gaming", SortOrder = 2 },
            new Category { Id = 3, Name = "Tablet", Slug = "tablet", Description = "May tinh bang", SortOrder = 3 },
            new Category { Id = 4, Name = "Phu kien", Slug = "phu-kien", Description = "Phu kien cong nghe", SortOrder = 4 }
        );

        // Seed shop config
        builder.Entity<ShopConfig>().HasData(
            new ShopConfig { Id = 1, Key = "ShopName", Value = "Nexora", Type = "string" },
            new ShopConfig { Id = 2, Key = "Phone", Value = "0123456789", Type = "string" },
            new ShopConfig { Id = 3, Key = "Email", Value = "contact@nexora.vn", Type = "string" },
            new ShopConfig { Id = 4, Key = "Address", Value = "TP. Ho Chi Minh, Viet Nam", Type = "string" }
        );

        // Seed vouchers
        builder.Entity<Voucher>().HasData(
            new Voucher
            {
                Id = 1, Code = "WELCOME10", Description = "Giảm 10% cho đơn đầu tiên",
                DiscountPercent = 10, MaxDiscountAmount = 500000, MinOrderAmount = 1000000,
                UsageLimit = 100, StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31)
            },
            new Voucher
            {
                Id = 2, Code = "NEXORA50K", Description = "Giảm 50.000đ cho đơn từ 500K",
                DiscountAmount = 50000, MinOrderAmount = 500000,
                UsageLimit = 200, StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31)
            },
            new Voucher
            {
                Id = 3, Code = "FREESHIP", Description = "Giảm 30.000đ phí vận chuyển",
                DiscountAmount = 30000, MinOrderAmount = 300000,
                UsageLimit = 500, StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31)
            }
        );
    }
}
