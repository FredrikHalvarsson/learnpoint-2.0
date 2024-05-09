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
    public class TeacherCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeacherCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeacherCourses.Include(t => t.Course).Include(t => t.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeacherCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCourses = await _context.TeacherCourses
                .Include(t => t.Course)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherCoursesId == id);
            if (teacherCourses == null)
            {
                return NotFound();
            }

            return View(teacherCourses);
        }

        // GET: TeacherCourses/Create
        public IActionResult Create()
        {
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["FKTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: TeacherCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherCoursesId,FKTeacherId,FKCourseId")] TeacherCourses teacherCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherCourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teacherCourses.FKCourseId);
            ViewData["FKTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teacherCourses.FKTeacherId);
            return View(teacherCourses);
        }

        // GET: TeacherCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCourses = await _context.TeacherCourses.FindAsync(id);
            if (teacherCourses == null)
            {
                return NotFound();
            }
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teacherCourses.FKCourseId);
            ViewData["FKTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teacherCourses.FKTeacherId);
            return View(teacherCourses);
        }

        // POST: TeacherCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherCoursesId,FKTeacherId,FKCourseId")] TeacherCourses teacherCourses)
        {
            if (id != teacherCourses.TeacherCoursesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherCoursesExists(teacherCourses.TeacherCoursesId))
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
            ViewData["FKCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teacherCourses.FKCourseId);
            ViewData["FKTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teacherCourses.FKTeacherId);
            return View(teacherCourses);
        }

        // GET: TeacherCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherCourses = await _context.TeacherCourses
                .Include(t => t.Course)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherCoursesId == id);
            if (teacherCourses == null)
            {
                return NotFound();
            }

            return View(teacherCourses);
        }

        // POST: TeacherCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherCourses = await _context.TeacherCourses.FindAsync(id);
            if (teacherCourses != null)
            {
                _context.TeacherCourses.Remove(teacherCourses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherCoursesExists(int id)
        {
            return _context.TeacherCourses.Any(e => e.TeacherCoursesId == id);
        }
    }
}
