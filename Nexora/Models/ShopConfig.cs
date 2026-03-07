using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class ShopConfig
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Key { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Value { get; set; }

    [MaxLength(20)]
    public string Type { get; set; } = "string";
}
