using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TqdLesson12.Data;
using TqdLesson12.Models;
using TqdLesson12.Services;

namespace TqdLesson12.Controllers;

public class ProductsController(CatalogContext context, ImageStorage imageStorage) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await context.Products.Include(product => product.Category)
            .OrderByDescending(product => product.CreatedDate).ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        await LoadCategories();
        return View(new Product());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories(product.CategoryId);
            return View(product);
        }

        try
        {
            product.Image = await imageStorage.SaveAsync(imageFile, "products");
        }
        catch (InvalidDataException exception)
        {
            ModelState.AddModelError(nameof(imageFile), exception.Message);
            await LoadCategories(product.CategoryId);
            return View(product);
        }

        product.CreatedDate = DateTime.Now;
        context.Products.Add(product);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã thêm sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound();
        await LoadCategories(product.CategoryId);
        return View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
    {
        if (id != product.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            await LoadCategories(product.CategoryId);
            return View(product);
        }

        var existing = await context.Products.FindAsync(id);
        if (existing is null) return NotFound();

        string? newImage;
        try
        {
            newImage = await imageStorage.SaveAsync(imageFile, "products");
        }
        catch (InvalidDataException exception)
        {
            ModelState.AddModelError(nameof(imageFile), exception.Message);
            product.Image = existing.Image;
            await LoadCategories(product.CategoryId);
            return View(product);
        }

        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.SalePrice = product.SalePrice;
        existing.Status = product.Status;
        existing.Description = product.Description;
        existing.CategoryId = product.CategoryId;
        if (newImage is not null)
        {
            imageStorage.Delete(existing.Image);
            existing.Image = newImage;
        }

        await context.SaveChangesAsync();
        TempData["Message"] = "Đã cập nhật sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var product = await context.Products.Include(item => item.Category)
            .FirstOrDefaultAsync(item => item.Id == id);
        return product is null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            imageStorage.Delete(product.Image);
        }

        TempData["Message"] = "Đã xóa sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategories(int? selectedId = null)
    {
        ViewBag.Categories = new SelectList(
            await context.Categories.OrderBy(category => category.Name).ToListAsync(),
            nameof(Category.Id), nameof(Category.Name), selectedId);
    }
}