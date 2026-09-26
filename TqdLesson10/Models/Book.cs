using System.ComponentModel.DataAnnotations;

namespace TqdLesson10.Models;

public class Book
{
    public int BookId { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Tên sách")]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(100)]
    [Display(Name = "Tác giả")]
    public string Author { get; set; } = string.Empty;

    [Range(0, 100000000)]
    [Display(Name = "Giá")]
    public decimal Price { get; set; }

    [Display(Name = "Năm phát hành")]
    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Ảnh bìa")]
    public string? Picture { get; set; }

    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Display(Name = "Nhà xuất bản")]
    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
}