using System.ComponentModel.DataAnnotations;

namespace Nexora.Models.ViewModels;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Vui long nhap ho ten")]
    [MaxLength(100)]
    [Display(Name = "Ho ten")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui long nhap so dien thoai")]
    [MaxLength(15)]
    [Display(Name = "So dien thoai")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui long nhap dia chi")]
    [MaxLength(500)]
    [Display(Name = "Dia chi giao hang")]
    public string Address { get; set; } = string.Empty;

    [MaxLength(500)]
    [Display(Name = "Ghi chu")]
    public string? Note { get; set; }
}
