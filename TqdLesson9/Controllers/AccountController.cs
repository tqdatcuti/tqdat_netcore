using Microsoft.AspNetCore.Mvc;
using TqdLesson9.Models;

namespace TqdLesson9.Controllers;

public class AccountController : Controller
{
    private static readonly List<Account> Accounts = new();

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Validate & Annotation";
        return View(new Account());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Account account)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Validate & Annotation";
            return View(account);
        }

        account.Id = Accounts.Count + 1;
        Accounts.Add(account);
        TempData["SuccessMessage"] = "Thông tin hợp lệ. Dữ liệu đã được lưu thành công.";

        return RedirectToAction(nameof(Index));
    }
}
