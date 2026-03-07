using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class Cart
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = null!;
    public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}
