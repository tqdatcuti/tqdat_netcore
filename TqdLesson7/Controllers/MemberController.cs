using TqdLesson7.Models;
using Microsoft.AspNetCore.Mvc;

namespace TqdLesson7.Controllers;

public class MemberController : Controller
{
    private static readonly List<Member> Members = new()
    {
        new Member { MemberId = 1, Username = "member1", Fullname = "Thành viên 1", Password = "123456", Email = "member1@gmail.com" },
        new Member { MemberId = 2, Username = "member2", Fullname = "Thành viên 2", Password = "123456", Email = "member2@gmail.com" },
        new Member { MemberId = 3, Username = "member3", Fullname = "Thành viên 3", Password = "123456", Email = "member3@gmail.com" }
    };

    public IActionResult Index()
    {
        ViewData["Title"] = "Danh sách thành viên";
        ViewBag.Members = Members;
        return View(Members);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Thêm mới thành viên";
        var member = new Member();
        return View(member);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Member member)
    {
        if (!ModelState.IsValid)
        {
            return View(member);
        }

        member.MemberId = Members.Count + 1;
        Members.Add(member);
        TempData["SuccessMessage"] = "Thêm thành viên thành công!";

        return RedirectToAction(nameof(Index));
    }
}
