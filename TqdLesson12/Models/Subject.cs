using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class Subject
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Tên môn học")]
    public string SubjectName { get; set; } = string.Empty;

    public ICollection<StudentMark> Marks { get; set; } = new List<StudentMark>();
}