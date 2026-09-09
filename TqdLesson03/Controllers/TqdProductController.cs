using Microsoft.AspNetCore.Mvc;
using TqdLesson03.Models;

namespace TqdLesson03.Controllers
{
    [Route("/danh-sach-san-pham")]
    public class TqdProductController : Controller
    {
        // Mock data
        private readonly List<TqdProduct> _products = new()
        {
            new TqdProduct
            {
                TqdProductId = "TQD-MB-001",
                TqdProductName = "iPhone 15 Pro Max 256GB",
                TqdYearRelease = 2023,
                TqdPrice = 29990000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-002",
                TqdProductName = "Samsung Galaxy S24 Ultra 512GB",
                TqdYearRelease = 2024,
                TqdPrice = 31490000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-003",
                TqdProductName = "Xiaomi 14 Ultra 5G",
                TqdYearRelease = 2024,
                TqdPrice = 27990000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-004",
                TqdProductName = "Google Pixel 8 Pro 128GB",
                TqdYearRelease = 2023,
                TqdPrice = 21500000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-005",
                TqdProductName = "OPPO Find N3 Flip 256GB",
                TqdYearRelease = 2023,
                TqdPrice = 19990000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-006",
                TqdProductName = "Samsung Galaxy Z Fold5 512GB",
                TqdYearRelease = 2023,
                TqdPrice = 34990000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-007",
                TqdProductName = "iPad Pro M4 11-inch Wi-Fi 256GB",
                TqdYearRelease = 2024,
                TqdPrice = 28990000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-008",
                TqdProductName = "Samsung Galaxy Tab S9 Ultra",
                TqdYearRelease = 2023,
                TqdPrice = 25490000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-009",
                TqdProductName = "ASUS ROG Phone 8 Pro 512GB",
                TqdYearRelease = 2024,
                TqdPrice = 28490000m
            },
            new TqdProduct
            {
                TqdProductId = "TQD-MB-010",
                TqdProductName = "Vivo X100 Pro 5G 256GB",
                TqdYearRelease = 2024,
                TqdPrice = 22990000m
            }
        };
        public IActionResult Index()
        {
            return Json(_products);
        }

        // Collection => view
        [Route("/all")]
        public IActionResult TqdGetAllProduct()
        {
            ViewData["products"] = _products;
            return View();
        }
    }
}
