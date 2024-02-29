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
using AutoMapper;
using Management.ViewModels.ClassModel;

namespace Management.Controllers
{
    [Authorize]
    public class ClassesController : Controller
    {
        private readonly ManagementContext _context;
        private readonly IMapper _mapper;

        public ClassesController(ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {

            return View(await _context.Class.Include(c => c.Students).OrderByDescending(c => c.Created).ToListAsync());
        }

        public IActionResult Enroll(int id)
        {
            var students = _context.Student.Include(c => c.Classes).ToList();
            ViewBag.StudentSelectList = new SelectList(
                    items: students,
                    dataValueField: nameof(Student.Id),
                    dataTextField: nameof(Student.FullName)
                );
            var model = new EnrollViewModel();
            model.Id = id;
            model.Name = _context.Class.FirstOrDefault(c => c.Id == id).Name;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll([FromForm] int Id, [FromForm] List<string> StudentSelectList)
        {
            List<Student> results = new List<Student>();
            foreach (var item in StudentSelectList)
            {
                results.Add(_context.Student.Find(Int32.Parse(item)));
            }
            var c = await _context.Class.FindAsync(Id);
            if (c.Students == null)
            {
                c.Students = [];
            }
            foreach (var item in results)
            {
                if (c.Students.Equals(item) == false)
                {
                    c.Students.Add(item);
                }
            }
            _context.Update(c);
            await _context.SaveChangesAsync();

            return Json(new { status = "success", message = "Successfully!" });
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var c = _mapper.Map<Class>(model);
                _context.Add(c);
                await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Created class!" });
            }
            return Json(new { status = "error", message = "Error!" });
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
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
                    var c = _mapper.Map<Class>(model);
                    _context.Update(c);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(model.Id))
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

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Class.FindAsync(id);
            if (@class != null)
            {
                _context.Class.Remove(@class);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> List(int id)
        {
            return RedirectToAction("Index", "Students");
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.Id == id);
        }
    }
}
