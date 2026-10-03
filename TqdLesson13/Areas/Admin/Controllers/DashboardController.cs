using Microsoft.AspNetCore.Mvc;

namespace TqdLesson13.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Trang quản trị";
        return View();
    }
}