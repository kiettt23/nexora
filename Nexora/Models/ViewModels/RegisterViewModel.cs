using System.ComponentModel.DataAnnotations;

namespace Nexora.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Vui long nhap ho ten")]
    [MaxLength(100)]
    [Display(Name = "Ho ten")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui long nhap email")]
    [EmailAddress(ErrorMessage = "Email khong hop le")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(15)]
    [Display(Name = "So dien thoai")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Vui long nhap mat khau")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Mat khau toi thieu 6 ky tu")]
    [Display(Name = "Mat khau")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui long xac nhan mat khau")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Mat khau xac nhan khong khop")]
    [Display(Name = "Xac nhan mat khau")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
