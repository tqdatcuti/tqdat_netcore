using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TqdLesson12.Data;
using TqdLesson12.Models;

namespace TqdLesson12.Controllers;

public class CategoriesController(CatalogContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var categories = await context.Categories
            .Include(category => category.Products)
            .OrderBy(category => category.Name)
            .ToListAsync();
        return View(categories);
    }

    public IActionResult Create() => View(new Category());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        category.CreatedDate = DateTime.Now;
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã thêm danh mục.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var category = await context.Categories.FindAsync(id);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category category)
    {
        if (id != category.Id) return NotFound();
        if (!ModelState.IsValid) return View(category);

        var existing = await context.Categories.FindAsync(id);
        if (existing is null) return NotFound();
        existing.Name = category.Name;
        existing.Status = category.Status;
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã cập nhật danh mục.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var category = await context.Categories.Include(item => item.Products)
            .FirstOrDefaultAsync(item => item.Id == id);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await context.Categories.Include(item => item.Products)
            .FirstOrDefaultAsync(item => item.Id == id);
        if (category is null) return RedirectToAction(nameof(Index));
        if (category.Products.Count > 0)
        {
            TempData["Error"] = "Không thể xóa danh mục đang có sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã xóa danh mục.";
        return RedirectToAction(nameof(Index));
    }
}