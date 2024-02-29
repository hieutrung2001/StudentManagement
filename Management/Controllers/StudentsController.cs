using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Management.Data;
using Management.Models;
using Microsoft.AspNetCore.Authorization;
using Management.ViewModels.StudentModel;
using AutoMapper;

namespace Management.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ManagementContext _context;
        private readonly IMapper _mapper;

        public StudentsController(ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                if (_context.Student != null) {
                    List<Student> results = [];
                    var students = await _context.Student.Include(s => s.Classes).OrderByDescending(s => s.Created).ToListAsync();
                    foreach (var item in students)
                    {
                        if (item.Classes?.Count > 0)
                        {
                            foreach (var y in item.Classes)
                            {
                                if (y.Id == id) { 
                                    results.Add(item);
                                }
                            }
                        }
                    }
                    return View(results);
                }
                return View();
            }
            return View(await _context.Student.Include(s => s.Classes).OrderByDescending(s => s.Created).ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(model);
                _context.Add(student);
                await _context.SaveChangesAsync();
                return Json(new { status="success", message="Created student!" });
            }
            return Json(new { status="error", message="Error!" });
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = _mapper.Map<Student>(model);
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { status = "success", message = "Edited Successfully!" });
            }
            return Json(new { status = "error", message = "Error!" });
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search(string term)
        {
            if (term != null)
            {
                return Json(new { item = _context.Student.Include(c => c.Classes).Where(c => c.FullName == term).ToList() });
            }
            return Json(new { item = _context.Student.Include(c => c.Classes).ToList() });
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
