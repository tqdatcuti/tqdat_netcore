using System.ComponentModel.DataAnnotations;

namespace TqdLesson10.Models;

public class Publisher
{
    public int PublisherId { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Tên nhà xuất bản")]
    public string PublisherName { get; set; } = string.Empty;

    [StringLength(50)]
    [Display(Name = "Số điện thoại")]
    public string? Phone { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}