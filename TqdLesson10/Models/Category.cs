using System.ComponentModel.DataAnnotations;

namespace TqdLesson10.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Tên danh mục")]
    public string CategoryName { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}