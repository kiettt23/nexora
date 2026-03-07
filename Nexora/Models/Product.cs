using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(5000)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal? OriginalPrice { get; set; }

    [MaxLength(100)]
    public string? Brand { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    // Nullable spec fields (null for accessories)
    [MaxLength(50)]
    public string? Storage { get; set; }

    [MaxLength(50)]
    public string? RAM { get; set; }

    [MaxLength(50)]
    public string? ScreenSize { get; set; }

    [MaxLength(100)]
    public string? CPU { get; set; }

    public int CategoryId { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFeatured { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public Category Category { get; set; } = null!;
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}
