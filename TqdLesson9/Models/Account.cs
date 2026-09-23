using System.ComponentModel.DataAnnotations;

namespace TqdLesson9.Models;

public class Account
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Họ và tên không được để trống")]
    [MinLength(6, ErrorMessage = "Họ tên ít nhất là 6 ký tự")]
    [MaxLength(20, ErrorMessage = "Họ tên tối đa 20 ký tự")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ email")]
    [Required(ErrorMessage = "Địa chỉ email không được để trống")]
    [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ thường trú")]
    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [StringLength(35, ErrorMessage = "Địa chỉ không vượt quá 35 ký tự")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Ảnh đại diện")]
    public string? Avatar { get; set; }

    [Display(Name = "Ngày sinh")]
    [Required(ErrorMessage = "Ngày sinh không được để trống")]
    [DataType(DataType.Date)]
    [AdultAge(ErrorMessage = "Bạn phải từ 18 tuổi trở lên")]
    public DateTime Birthday { get; set; }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class AdultAgeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime birthday)
        {
            return false;
        }

        return birthday <= DateTime.Today.AddYears(-18);
    }
}
