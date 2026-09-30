using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class Student
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Họ tên")]
    public string StudentName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100)]
    [Display(Name = "Email")]
    public string StudentEmail { get; set; } = string.Empty;

    [Required, StringLength(50)]
    [Display(Name = "Điện thoại")]
    public string StudentPhone { get; set; } = string.Empty;

    [Required, StringLength(150)]
    [Display(Name = "Địa chỉ")]
    public string StudentAddress { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Ảnh đại diện")]
    public string? StudentAvatar { get; set; }

    [Required, DataType(DataType.Date)]
    [Range(typeof(DateTime), "1/1/1900", "12/31/2100")]
    [Display(Name = "Ngày sinh")]
    public DateTime StudentBirthday { get; set; }

    [Display(Name = "Lớp")]
    public int ClassId { get; set; }

    public StudentClass? Class { get; set; }
    public ICollection<StudentMark> Marks { get; set; } = new List<StudentMark>();
}