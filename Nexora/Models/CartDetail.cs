using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Models;

public class CartDetail
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    [Range(1, 99)]
    public int Quantity { get; set; } = 1;

    [Column(TypeName = "decimal(18,0)")]
    public decimal UnitPrice { get; set; }

    // Navigation
    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
