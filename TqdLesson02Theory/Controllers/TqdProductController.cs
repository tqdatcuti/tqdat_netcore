using Microsoft.AspNetCore.Mvc;
using TqdLesson02Theory.Models;

namespace TqdLesson02Theory.Controllers
{
    public class TqdProductController : Controller
    {
        public IActionResult TqdIndex()
        {
            // Dữ liệu lưu trong đối  tượng: ViewBag, ViewData, TempData
            ViewBag.name = "Trịnh Văn Chung";
            ViewData["productVD"] = "Laptop Dell Vostro";
            TempData["UNI"] = "Trường Đại học Nguyễn Trãi - NTU";

            return View();
        }

        public IActionResult GetProduct()
        {
            // Tạo mock data product
            TqdProduct tqdProduct = new TqdProduct()
            {
                ProductID = "2400012323",
                ProductName = "Trịnh Văn Chung",
                YearRelease=1979,
                Price = 1000
            };

            ViewBag.product = tqdProduct;
            ViewData["product"] = tqdProduct;

            return View("product");
        }
    }
}
