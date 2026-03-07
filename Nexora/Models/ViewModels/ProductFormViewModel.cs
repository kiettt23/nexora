using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Nexora.Models.ViewModels;

public class ProductFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui long nhap ten san pham")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(5000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Vui long nhap gia")]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    [MaxLength(100)]
    public string? Brand { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [MaxLength(50)]
    public string? Storage { get; set; }

    [MaxLength(50)]
    public string? RAM { get; set; }

    [MaxLength(50)]
    public string? ScreenSize { get; set; }

    [MaxLength(100)]
    public string? CPU { get; set; }

    [Required(ErrorMessage = "Vui long chon danh muc")]
    public int CategoryId { get; set; }

    public bool IsFeatured { get; set; }

    public string? ImageUrls { get; set; }

    public List<IFormFile>? ImageFiles { get; set; }
}
