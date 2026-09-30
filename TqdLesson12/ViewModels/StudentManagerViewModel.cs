using TqdLesson12.Models;

namespace TqdLesson12.ViewModels;

public class StudentManagerViewModel
{
    public List<StudentClass> Classes { get; set; } = [];
    public List<Student> Students { get; set; } = [];
    public List<Subject> Subjects { get; set; } = [];
    public List<StudentMark> Marks { get; set; } = [];
    public StudentClass? EditingClass { get; set; }
    public Student? EditingStudent { get; set; }
    public Subject? EditingSubject { get; set; }
    public StudentMark? EditingMark { get; set; }
}