using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class StudentClass
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Tên lớp")]
    public string ClassName { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}