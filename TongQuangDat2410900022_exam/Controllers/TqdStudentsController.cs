using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TongQuangDat2410900022_exam.Data;
using TongQuangDat2410900022_exam.Models;

namespace TongQuangDat2410900022_exam.Controllers
{
    public class TqdStudentsController : Controller
    {
        private readonly TqdStudentContext _context;

        public TqdStudentsController(TqdStudentContext context)
        {
            _context = context;
        }

        // GET: TqdStudents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TqdStudents.ToListAsync());
        }

        // GET: TqdStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tqdStudent = await _context.TqdStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tqdStudent == null)
            {
                return NotFound();
            }

            return View(tqdStudent);
        }

        // GET: TqdStudents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TqdStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TqdName,TqdGender,TqdBirthDay,TqdEmail,TqdPhone,TqdActive")] TqdStudent tqdStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tqdStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tqdStudent);
        }

        // GET: TqdStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tqdStudent = await _context.TqdStudents.FindAsync(id);
            if (tqdStudent == null)
            {
                return NotFound();
            }
            return View(tqdStudent);
        }

        // POST: TqdStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TqdName,TqdGender,TqdBirthDay,TqdEmail,TqdPhone,TqdActive")] TqdStudent tqdStudent)
        {
            if (id != tqdStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tqdStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TqdStudentExists(tqdStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tqdStudent);
        }

        // GET: TqdStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tqdStudent = await _context.TqdStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tqdStudent == null)
            {
                return NotFound();
            }

            return View(tqdStudent);
        }

        // POST: TqdStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tqdStudent = await _context.TqdStudents.FindAsync(id);
            if (tqdStudent != null)
            {
                _context.TqdStudents.Remove(tqdStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TqdStudentExists(int id)
        {
            return _context.TqdStudents.Any(e => e.Id == id);
        }
    }
}
