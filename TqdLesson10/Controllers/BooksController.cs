using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TqdLesson10.Data;
using TqdLesson10.Models;

namespace TqdLesson10.Controllers;

public class BooksController(BookStoreContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, int? categoryId)
    {
        var books = context.Books.Include(book => book.Category).Include(book => book.Publisher).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            books = books.Where(book => book.Title.Contains(search) || book.Author.Contains(search));
        }
        if (categoryId.HasValue)
        {
            books = books.Where(book => book.CategoryId == categoryId.Value);
        }

        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.Categories = new SelectList(await context.Categories.OrderBy(category => category.CategoryName).ToListAsync(), "CategoryId", "CategoryName", categoryId);
        return View(await books.OrderBy(book => book.Title).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : View(await context.Books.Include(book => book.Category).Include(book => book.Publisher).FirstOrDefaultAsync(book => book.BookId == id));

    public async Task<IActionResult> Create()
    {
        await LoadLookups();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid)
        {
            await LoadLookups(book);
            return View(book);
        }
        context.Add(book);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã thêm sách thành công.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var book = await context.Books.FindAsync(id);
        if (book is null) return NotFound();
        await LoadLookups(book);
        return View(book);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.BookId) return NotFound();
        if (!ModelState.IsValid)
        {
            await LoadLookups(book);
            return View(book);
        }
        context.Update(book);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã cập nhật sách thành công.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : View(await context.Books.Include(book => book.Category).Include(book => book.Publisher).FirstOrDefaultAsync(book => book.BookId == id));

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book is not null) context.Books.Remove(book);
        await context.SaveChangesAsync();
        TempData["Message"] = "Đã xóa sách thành công.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadLookups(Book? book = null)
    {
        ViewBag.Categories = new SelectList(await context.Categories.OrderBy(category => category.CategoryName).ToListAsync(), "CategoryId", "CategoryName", book?.CategoryId);
        ViewBag.Publishers = new SelectList(await context.Publishers.OrderBy(publisher => publisher.PublisherName).ToListAsync(), "PublisherId", "PublisherName", book?.PublisherId);
    }
}