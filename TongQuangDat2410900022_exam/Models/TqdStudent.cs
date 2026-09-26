using System.ComponentModel.DataAnnotations;

namespace TongQuangDat2410900022_exam.Models;

public class TqdStudent : IValidatableObject
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Họ và tên")]
    public string TqdName { get; set; } = string.Empty;

    [Required, StringLength(10)]
    [Display(Name = "Giới tính")]
    public string TqdGender { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")]
    public DateTime TqdBirthDay { get; set; }

    [EmailAddress, StringLength(254)]
    [Display(Name = "Email")]
    public string? TqdEmail { get; set; }

    [Phone, StringLength(20)]
    [Display(Name = "Điện thoại")]
    public string? TqdPhone { get; set; }

    [Display(Name = "Đang học")]
    public bool TqdActive { get; set; } = true;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TqdBirthDay < new DateTime(1000, 1, 1))
        {
            yield return new ValidationResult(
                "Ngày sinh phải từ 01/01/1000 trở đi.",
                new[] { nameof(TqdBirthDay) });
        }
    }
}