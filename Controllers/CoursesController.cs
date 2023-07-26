using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using online_course_platform.Data;
using online_course_platform.Models;

namespace online_course_platform.Controllers
{
    public class CoursesController : Controller
    {
        private readonly OnlineCoursesContext _context;

        public CoursesController(OnlineCoursesContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var onlineCoursesContext = _context.Courses.Include(c => c.Instructor);
            return View(await onlineCoursesContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            var users = _context.Users.ToList();

            var roles = _context.Roles.ToList();

            var usersInfo = (from user in users
                             join role in roles on user.Id equals role.UserId
                             where role.Role1 == "Instructor"
                             select new
                             {
                                 Id = user.Id,
                                 firstName = user.FirstName,
                                 lastName = user.LastName,
                             }).ToList();


            System.Console.WriteLine(usersInfo.Count);


            ViewData["InstructorId"] = new SelectList(usersInfo, "Id", "Id");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,Description,StartDate,EndDate,InstructorId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", course.InstructorId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", course.InstructorId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,Description,StartDate,EndDate,InstructorId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", course.InstructorId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'OnlineCoursesContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
