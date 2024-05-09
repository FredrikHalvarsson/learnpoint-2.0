using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using learnpoint_2._0.Data;
using learnpoint_2._0.Models;

namespace learnpoint_2._0.Controllers
{
    public class ClassCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassCourses.Include(c => c.Course).Include(c => c.SchoolClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classCourses = await _context.ClassCourses
                .Include(c => c.Course)
                .Include(c => c.SchoolClass)
                .FirstOrDefaultAsync(m => m.ClassCourseId == id);
            if (classCourses == null)
            {
                return NotFound();
            }

            return View(classCourses);
        }

        // GET: ClassCourses/Create
        public IActionResult Create()
        {
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["FKSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassTitle");
            return View();
        }

        // POST: ClassCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassCourseId,FKSchoolClassId,FKCourseId")] ClassCourses classCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", classCourses.FKCourseId);
            ViewData["FKSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassTitle", classCourses.FKSchoolClassId);
            return View(classCourses);
        }

        // GET: ClassCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classCourses = await _context.ClassCourses.FindAsync(id);
            if (classCourses == null)
            {
                return NotFound();
            }
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", classCourses.FKCourseId);
            ViewData["FKSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassTitle", classCourses.FKSchoolClassId);
            return View(classCourses);
        }

        // POST: ClassCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassCourseId,FKSchoolClassId,FKCourseId")] ClassCourses classCourses)
        {
            if (id != classCourses.ClassCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassCoursesExists(classCourses.ClassCourseId))
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
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", classCourses.FKCourseId);
            ViewData["FKSchoolClassId"] = new SelectList(_context.SchoolClasses, "SchoolClassId", "ClassTitle", classCourses.FKSchoolClassId);
            return View(classCourses);
        }

        // GET: ClassCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classCourses = await _context.ClassCourses
                .Include(c => c.Course)
                .Include(c => c.SchoolClass)
                .FirstOrDefaultAsync(m => m.ClassCourseId == id);
            if (classCourses == null)
            {
                return NotFound();
            }

            return View(classCourses);
        }

        // POST: ClassCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classCourses = await _context.ClassCourses.FindAsync(id);
            if (classCourses != null)
            {
                _context.ClassCourses.Remove(classCourses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassCoursesExists(int id)
        {
            return _context.ClassCourses.Any(e => e.ClassCourseId == id);
        }
    }
}
