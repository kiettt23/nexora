using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipping,
    Delivered,
    Cancelled
}

public class Order
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string OrderCode { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(15)]
    public string Phone { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Note { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    public decimal DiscountAmount { get; set; }

    [MaxLength(20)]
    public string? VoucherCode { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [MaxLength(50)]
    public string PaymentMethod { get; set; } = "COD";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
