using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class Banner
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Tên banner")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    [Display(Name = "Ảnh")]
    public string? Image { get; set; }

    [StringLength(1000)]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Range(0, 255)]
    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;
}