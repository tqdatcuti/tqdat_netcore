using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TqdLesson12.Data;
using TqdLesson12.Models;
using TqdLesson12.Services;

namespace TqdLesson12.Controllers;

public class BannersController(CatalogContext context, ImageStorage imageStorage) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Banners.OrderByDescending(banner => banner.CreatedDate).ToListAsync());
    }

    public IActionResult Create() => View(new Banner());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Banner banner, IFormFile? imageFile)
    {
        if (!ModelState.IsValid) return View(banner);
        try
        {
            banner.Image = await imageStorage.SaveAsync(imageFile, "banners");
        }
        catch (InvalidDataException exception)
        {
            ModelState.AddModelError(nameof(imageFile), exception.Message);
            return View(banner);
        }

        banner.CreatedDate = DateTime.Now;
        context.Banners.Add(banner);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã thêm banner.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var banner = await context.Banners.FindAsync(id);
        return banner is null ? NotFound() : View(banner);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Banner banner, IFormFile? imageFile)
    {
        if (id != banner.Id) return NotFound();
        if (!ModelState.IsValid) return View(banner);
        var existing = await context.Banners.FindAsync(id);
        if (existing is null) return NotFound();

        string? newImage;
        try
        {
            newImage = await imageStorage.SaveAsync(imageFile, "banners");
        }
        catch (InvalidDataException exception)
        {
            ModelState.AddModelError(nameof(imageFile), exception.Message);
            banner.Image = existing.Image;
            return View(banner);
        }

        existing.Name = banner.Name;
        existing.Description = banner.Description;
        existing.Status = banner.Status;
        if (newImage is not null)
        {
            imageStorage.Delete(existing.Image);
            existing.Image = newImage;
        }

        await context.SaveChangesAsync();
        TempData["Message"] = "Đã cập nhật banner.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var banner = await context.Banners.FindAsync(id);
        return banner is null ? NotFound() : View(banner);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var banner = await context.Banners.FindAsync(id);
        if (banner is not null)
        {
            context.Banners.Remove(banner);
            await context.SaveChangesAsync();
            imageStorage.Delete(banner.Image);
        }

        TempData["Message"] = "Đã xóa banner.";
        return RedirectToAction(nameof(Index));
    }
}