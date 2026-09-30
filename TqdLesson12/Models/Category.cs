using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class Category
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 255)]
    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}