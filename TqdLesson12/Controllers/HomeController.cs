using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TqdLesson12.Data;
using TqdLesson12.Models;
using TqdLesson12.ViewModels;

namespace TqdLesson12.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CatalogContext _context;

    public HomeController(ILogger<HomeController> logger, CatalogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new CatalogHomeViewModel
        {
            Banners = await _context.Banners.Where(banner => banner.Status == 1)
                .OrderByDescending(banner => banner.CreatedDate).ToListAsync(),
            Products = await _context.Products.Include(product => product.Category)
                .Where(product => product.Status == 1)
                .OrderByDescending(product => product.CreatedDate).Take(8).ToListAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
