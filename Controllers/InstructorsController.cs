using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_course_platform.Data;
using online_course_platform.Models;
using online_course_platform.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Security.Cryptography.Xml;

namespace online_course_platform.Controllers
{
    [SessionCheck]
    public class InstructorsController : Controller
    {
        private readonly OnlineCoursesContext _context;

        public InstructorsController(OnlineCoursesContext context)
        {
            _context = context;
        }

        // GET
        // Instructors/
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            var roles = await _context.Roles.ToListAsync();

            var usersInfo = (from user in users
                             join role in roles on user.Id equals role.UserId
                             where role.Role1 == "Instructor"
                             select new
                             {
                                 ID = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 BirthDate = user.BirthDate,
                                 Gender = user.Gender ? "Male" : "Female",
                                 UserName = user.UserName,
                                 UserRole = role.Role1
                             }).ToList();

            dynamic model = new ExpandoObject();

            model.Users = usersInfo;

            return View(model);
        }

        // GET
        // Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var context = _context.Courses.Include(c => c.Instructor);

            var courses = await context.ToListAsync();

            var teaches = (from course in courses
                           where course.Instructor.Id == id
                           select new
                           {
                               Id = course.Id,
                               CourseName = course.CourseName,
                               Description = course.Description,
                               StartDate = course.StartDate,
                               EndDate = course.EndDate,
                               Instructor = course.Instructor,

                           }).ToList();



            dynamic model = new ExpandoObject();

            model.User = user;
            model.Courses = teaches;
            return View(model);
        }

        // GET
        // Instructors/Students/CourseId
        public async Task<IActionResult> Students(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }


            var coursesContext = _context.Courses.Include(c => c.Enrolleds);

            var allCourses = await coursesContext.ToListAsync();

            var course = (from c in allCourses
                          where c.Id == id
                          select new
                          {
                              Enrolleds = c.Enrolleds,
                              courseId = c.Id,
                              courseName = c.CourseName
                          }).ToList();



            dynamic model = new ExpandoObject();

            model.Course = course[0];
            return View(model);

        }


        // GET
        // Instructors/Grade/StudentID/CourseID
        public async Task<IActionResult> Grade(int? student, int? course)
        {
            if (student == null || course == null || _context == null)
            {
                return NotFound();
            }

            var coursesContext = _context.Courses.Include(c => c.Enrolleds);

            var courseInfo = await coursesContext.FirstOrDefaultAsync(c => c.Id == course);

            var studentInfo = await _context.Users.FirstOrDefaultAsync(s => s.Id == student);

            var allEnrolleds = courseInfo.Enrolleds;

            var enroll = (from en in allEnrolleds
                          where en.StudentId == student
                          select new
                          {
                              Grade = en.Grade,
                          }).ToList();


            dynamic model = new ExpandoObject();

            try
            {

                model.Course = courseInfo;
                model.Student = studentInfo;
                model.Grade = enroll[0].Grade;
            }
            catch
            {
                model.Course = courseInfo;
                model.Student = studentInfo;
                model.Grade = null;
            }



            return View(model);

        }



    }
}