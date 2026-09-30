using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TqdLesson12.Data;
using TqdLesson12.Models;
using TqdLesson12.Services;
using TqdLesson12.ViewModels;

namespace TqdLesson12.Controllers;

public class StudentManagerController(StudentManagerContext context, ImageStorage imageStorage) : Controller
{
    public async Task<IActionResult> Index(int? editClassId, int? editStudentId, int? editSubjectId,
        int? editStudentMarkStudentId, int? editStudentMarkSubjectId)
    {
        var model = new StudentManagerViewModel
        {
            Classes = await context.Classes.Include(item => item.Students).OrderBy(item => item.ClassName).ToListAsync(),
            Students = await context.Students.Include(item => item.Class).OrderBy(item => item.StudentName).ToListAsync(),
            Subjects = await context.Subjects.OrderBy(item => item.SubjectName).ToListAsync(),
            Marks = await context.Marks.Include(item => item.Student).Include(item => item.Subject)
                .OrderBy(item => item.Student!.StudentName).ThenBy(item => item.Subject!.SubjectName).ToListAsync(),
            EditingClass = editClassId is null ? null : await context.Classes.FindAsync(editClassId),
            EditingStudent = editStudentId is null ? null : await context.Students.FindAsync(editStudentId),
            EditingSubject = editSubjectId is null ? null : await context.Subjects.FindAsync(editSubjectId),
            EditingMark = editStudentMarkStudentId is null || editStudentMarkSubjectId is null
                ? null
                : await context.Marks.FindAsync(editStudentMarkSubjectId, editStudentMarkStudentId)
        };

        ViewBag.Classes = new SelectList(model.Classes, nameof(StudentClass.Id), nameof(StudentClass.ClassName), model.EditingStudent?.ClassId);
        ViewBag.Students = new SelectList(model.Students, nameof(Student.Id), nameof(Student.StudentName), model.EditingMark?.StudentId);
        ViewBag.Subjects = new SelectList(model.Subjects, nameof(Subject.Id), nameof(Subject.SubjectName), model.EditingMark?.SubjectId);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveClass(StudentClass item)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Tên lớp không hợp lệ.";
            return RedirectToAction(nameof(Index));
        }

        if (item.Id == 0) context.Classes.Add(item);
        else
        {
            var existing = await context.Classes.FindAsync(item.Id);
            if (existing is null) return NotFound();
            existing.ClassName = item.ClassName;
        }

        await context.SaveChangesAsync();
        TempData["Message"] = "Đã lưu lớp học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteClass(int id)
    {
        var item = await context.Classes.Include(current => current.Students).FirstOrDefaultAsync(current => current.Id == id);
        if (item is null) return RedirectToAction(nameof(Index));
        if (item.Students.Count > 0)
        {
            TempData["Error"] = "Không thể xóa lớp đang có học viên.";
            return RedirectToAction(nameof(Index));
        }

        context.Classes.Remove(item);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã xóa lớp học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveStudent(Student item, IFormFile? avatarFile)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Vui lòng kiểm tra lại thông tin học viên.";
            return RedirectToAction(nameof(Index));
        }

        if (await context.Students.AnyAsync(student => student.Id != item.Id &&
            (student.StudentEmail == item.StudentEmail || student.StudentPhone == item.StudentPhone)))
        {
            TempData["Error"] = "Email và số điện thoại của học viên phải là duy nhất.";
            return RedirectToAction(nameof(Index));
        }

        var existing = item.Id == 0 ? null : await context.Students.FindAsync(item.Id);
        if (item.Id != 0 && existing is null) return NotFound();
        if (avatarFile is null && existing is null && string.IsNullOrWhiteSpace(item.StudentAvatar))
        {
            TempData["Error"] = "Vui lòng chọn ảnh đại diện.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var avatar = await imageStorage.SaveAsync(avatarFile, "students");
            if (avatar is not null)
            {
                imageStorage.Delete(existing?.StudentAvatar);
                item.StudentAvatar = avatar;
            }
        }
        catch (InvalidDataException exception)
        {
            TempData["Error"] = exception.Message;
            return RedirectToAction(nameof(Index));
        }

        if (existing is null) context.Students.Add(item);
        else
        {
            existing.StudentName = item.StudentName;
            existing.StudentEmail = item.StudentEmail;
            existing.StudentPhone = item.StudentPhone;
            existing.StudentAddress = item.StudentAddress;
            existing.StudentBirthday = item.StudentBirthday;
            existing.ClassId = item.ClassId;
            if (avatarFile is not null && !string.IsNullOrWhiteSpace(item.StudentAvatar))
                existing.StudentAvatar = item.StudentAvatar;
        }

        await context.SaveChangesAsync();
        TempData["Message"] = "Đã lưu thông tin học viên.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var item = await context.Students.FindAsync(id);
        if (item is not null)
        {
            var marks = await context.Marks.Where(mark => mark.StudentId == id).ToListAsync();
            context.Marks.RemoveRange(marks);
            context.Students.Remove(item);
            await context.SaveChangesAsync();
            imageStorage.Delete(item.StudentAvatar);
        }

        TempData["Message"] = "Đã xóa học viên.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveSubject(Subject item)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Tên môn học không hợp lệ.";
            return RedirectToAction(nameof(Index));
        }

        if (await context.Subjects.AnyAsync(subject => subject.Id != item.Id && subject.SubjectName == item.SubjectName))
        {
            TempData["Error"] = "Tên môn học không được trùng.";
            return RedirectToAction(nameof(Index));
        }

        if (item.Id == 0) context.Subjects.Add(item);
        else
        {
            var existing = await context.Subjects.FindAsync(item.Id);
            if (existing is null) return NotFound();
            existing.SubjectName = item.SubjectName;
        }

        await context.SaveChangesAsync();
        TempData["Message"] = "Đã lưu môn học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var item = await context.Subjects.FindAsync(id);
        if (item is not null)
        {
            var marks = await context.Marks.Where(mark => mark.SubjectId == id).ToListAsync();
            context.Marks.RemoveRange(marks);
            context.Subjects.Remove(item);
            await context.SaveChangesAsync();
        }

        TempData["Message"] = "Đã xóa môn học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveMark(StudentMark item, int originalStudentId, int originalSubjectId)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Điểm phải hợp lệ và nằm trong khoảng 0 đến 10.";
            return RedirectToAction(nameof(Index));
        }

        var existing = originalStudentId > 0 && originalSubjectId > 0
            ? await context.Marks.FindAsync(originalSubjectId, originalStudentId)
            : null;
        if (existing is not null)
        {
            if (item.StudentId != originalStudentId || item.SubjectId != originalSubjectId)
            {
                var duplicate = await context.Marks.FindAsync(item.SubjectId, item.StudentId);
                if (duplicate is not null)
                {
                    TempData["Error"] = "Học viên đã có điểm cho môn học này.";
                    return RedirectToAction(nameof(Index));
                }
                context.Marks.Remove(existing);
                await context.SaveChangesAsync();
                context.Marks.Add(item);
            }
            else
            {
                existing.Score = item.Score;
            }
        }
        else
        {
            if (await context.Marks.FindAsync(item.SubjectId, item.StudentId) is not null)
            {
                TempData["Error"] = "Học viên đã có điểm cho môn học này.";
                return RedirectToAction(nameof(Index));
            }
            context.Marks.Add(item);
        }

        await context.SaveChangesAsync();
        TempData["Message"] = "Đã lưu điểm.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMark(int studentId, int subjectId)
    {
        var item = await context.Marks.FindAsync(subjectId, studentId);
        if (item is not null)
        {
            context.Marks.Remove(item);
            await context.SaveChangesAsync();
        }

        TempData["Message"] = "Đã xóa điểm.";
        return RedirectToAction(nameof(Index));
    }
}