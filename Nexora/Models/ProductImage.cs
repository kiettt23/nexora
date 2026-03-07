using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Required, MaxLength(500)]
    public string ImagePath { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public bool IsMain { get; set; }

    // Navigation
    public Product Product { get; set; } = null!;
}
