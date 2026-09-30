using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    [Display(Name = "Ảnh")]
    public string? Image { get; set; }

    [Range(typeof(decimal), "0", "999999999")]
    [Display(Name = "Giá")]
    public decimal Price { get; set; }

    [Range(typeof(decimal), "0", "999999999")]
    [Display(Name = "Giá khuyến mãi")]
    public decimal SalePrice { get; set; }

    [Range(0, 255)]
    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [StringLength(1000)]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Category? Category { get; set; }
}