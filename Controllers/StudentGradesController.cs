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
    public class StudentGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Grades.Include(s => s.Course).Include(s => s.GradingTeacher).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.Grades
                .Include(s => s.Course)
                .Include(s => s.GradingTeacher)
                .Include(s => s.Student)
                .Include( s=> s.Grade)
                .FirstOrDefaultAsync(m => m.StudentGradeId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // GET: StudentGrades/Create
        public IActionResult Create()
        {
            ViewData["FkCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName");
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentGradeId,Grade,FkCourseId,FkStudentId,FkTeacherId")] StudentGrade studentGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", studentGrade.FkCourseId);
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", studentGrade.FkTeacherId);
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", studentGrade.FkStudentId);
            return View(studentGrade);
        }

        // GET: StudentGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.Grades.FindAsync(id);
            if (studentGrade == null)
            {
                return NotFound();
            }
            ViewData["FkCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", studentGrade.FkCourseId);
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", studentGrade.FkTeacherId);
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", studentGrade.FkStudentId);
            return View(studentGrade);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentGradeId,Grade,FkCourseId,FkStudentId,FkTeacherId")] StudentGrade studentGrade)
        {
            if (id != studentGrade.StudentGradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeExists(studentGrade.StudentGradeId))
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
            ViewData["FkCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", studentGrade.FkCourseId);
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", studentGrade.FkTeacherId);
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", studentGrade.FkStudentId);
            return View(studentGrade);
        }

        // GET: StudentGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.Grades
                .Include(s => s.Course)
                .Include(s => s.GradingTeacher)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentGradeId == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGrade = await _context.Grades.FindAsync(id);
            if (studentGrade != null)
            {
                _context.Grades.Remove(studentGrade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradeExists(int id)
        {
            return _context.Grades.Any(e => e.StudentGradeId == id);
        }
    }
}
