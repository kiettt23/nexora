using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Models;

public class Voucher
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal DiscountAmount { get; set; }

    public int DiscountPercent { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal MinOrderAmount { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal MaxDiscountAmount { get; set; }

    public int UsageLimit { get; set; }
    public int UsedCount { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsValid => IsActive && UsedCount < UsageLimit && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
}
