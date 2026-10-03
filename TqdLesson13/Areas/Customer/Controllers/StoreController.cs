using Microsoft.AspNetCore.Mvc;

namespace TqdLesson13.Areas.Customer.Controllers;

[Area("Customer")]
public class StoreController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Trang khách hàng";
        return View();
    }
}