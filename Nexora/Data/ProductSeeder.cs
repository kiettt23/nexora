using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data;

public static class ProductSeeder
{
    public static async Task SeedProductsAsync(ApplicationDbContext db)
    {
        if (await db.Products.AnyAsync())
            return;

        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "products-seed.json");
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine("products-seed.json not found, skipping product seed.");
            return;
        }

        var json = await File.ReadAllTextAsync(jsonPath);
        var items = JsonSerializer.Deserialize<List<ProductSeedItem>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        if (items == null) return;

        foreach (var item in items)
        {
            var product = new Product
            {
                Name = item.Name,
                Slug = item.Slug,
                Description = item.Description ?? "",
                Price = item.Price,
                OriginalPrice = item.OriginalPrice,
                Brand = item.Brand ?? "",
                Color = item.Color,
                Storage = item.Storage,
                RAM = item.Ram,
                ScreenSize = item.ScreenSize,
                CPU = item.Cpu,
                CategoryId = item.CategoryId,
                IsActive = true,
                IsFeatured = item.IsFeatured,
                CreatedAt = DateTime.UtcNow
            };

            if (item.Images != null)
            {
                for (int i = 0; i < item.Images.Count; i++)
                {
                    product.Images.Add(new ProductImage
                    {
                        ImagePath = item.Images[i],
                        SortOrder = i,
                        IsMain = i == 0
                    });
                }
            }

            db.Products.Add(product);
        }

        await db.SaveChangesAsync();
        Console.WriteLine($"Seeded {items.Count} products.");
    }

    private class ProductSeedItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string? Brand { get; set; }
        public string? Color { get; set; }
        public string? Storage { get; set; }
        public string? Ram { get; set; }
        public string? ScreenSize { get; set; }
        public string? Cpu { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public List<string>? Images { get; set; }
    }
}
