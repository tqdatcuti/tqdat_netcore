using System.ComponentModel.DataAnnotations;

namespace TqdLesson12.Models;

public class StudentMark
{
    [Display(Name = "Môn học")]
    public int SubjectId { get; set; }

    [Display(Name = "Học viên")]
    public int StudentId { get; set; }

    [Range(0, 10)]
    [Display(Name = "Điểm")]
    public double Score { get; set; }

    public Subject? Subject { get; set; }
    public Student? Student { get; set; }
}