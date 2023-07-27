using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_course_platform.Data;
using online_course_platform.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Security.Cryptography.Xml;

namespace online_course_platform.Controllers
{
    public class StudentsController : Controller
    {
        private readonly OnlineCoursesContext _context;

        public StudentsController(OnlineCoursesContext context)
        {
            _context = context;
        }

        // GET
        // Students
        public async Task<IActionResult> Index()
        {

            var users = await _context.Users.ToListAsync();

            var roles = await _context.Roles.ToListAsync();

            var usersInfo = (from user in users
                             join role in roles on user.Id equals role.UserId
                             where role.Role1 == "Student"
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
        // Students/Details/5
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
            var enrolls = await _context.Enrolleds.ToListAsync();


            var alreadyEnrolled = (from enroll in enrolls
                                   where enroll.StudentId == id
                                   select enroll.CourseId).ToList();

            var enrolleds = (from course in courses
                             join cId in alreadyEnrolled on course.Id equals cId
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
            model.Courses = enrolleds;
            return View(model);
        }


        // GET
        // Students/Enroll/5
        public async Task<IActionResult> Enroll(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var context = _context.Courses.Include(c => c.Instructor);


            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            var courses = await context.ToListAsync();
            var enrolls = await _context.Enrolleds.ToListAsync();


            var alreadyEnrolled = (from enroll in enrolls
                                   where enroll.StudentId == id
                                   select enroll.CourseId).ToList();


            var coursesIds = (from course in courses
                              select course.Id).ToList();


            coursesIds = coursesIds.Except(alreadyEnrolled).ToList();

            var availableCourses = (from course in courses
                                    join cId in coursesIds on course.Id equals cId
                                    select new
                                    {
                                        Id = course.Id,
                                        CourseName = course.CourseName,
                                        Description = course.Description,
                                        StartDate = course.StartDate,
                                        EndDate = course.EndDate,
                                        Instructor = course.Instructor,
                                    }).ToList();


            if (user == null)
            {
                return NotFound();
            }


            dynamic model = new ExpandoObject();

            model.User = user;
            model.Courses = availableCourses;
            return View(model);
        }

        // POST
        // Students/Enroll/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int id, IFormCollection collection)
        {
            try
            {

                string[] courseInfo = collection["CourseId"].ToString().Split(" ");

                var courseID = int.Parse(courseInfo[0]);
                var instructorID = int.Parse(courseInfo[1]);


                Enrolled enroll = new()
                {
                    CourseId = courseID,
                    StudentId = id,
                    InstructorId = instructorID,
                };



                _context.Add(enroll);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {
                System.Console.WriteLine("------------exception-start-------");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("------------exception--end----------");
                ViewData["Error"] = "a problem has occurred, please try again";

                return View();
            }
        }

        // GET
        // Students/Delist/5
        public async Task<IActionResult> Delist(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }


            var context = _context.Courses.Include(c => c.Instructor);


            var courses = await context.ToListAsync();
            var enrolls = await _context.Enrolleds.ToListAsync();


            var alreadyEnrolled = (from enroll in enrolls
                                   where enroll.StudentId == id
                                   select enroll.CourseId).ToList();

            var enrolleds = (from course in courses
                             join cId in alreadyEnrolled on course.Id equals cId
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

            model.Courses = enrolleds;
            return View(model);
        }

        // POST
        // Users/Delist/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delist(int id, IFormCollection collection)
        {

            try
            {
                string[] courseInfo = collection["CourseId"].ToString().Split(" ");

                var courseID = int.Parse(courseInfo[0]);
                var instructorID = int.Parse(courseInfo[1]);

                var enrolled = await _context.Enrolleds.FirstOrDefaultAsync(e => e.StudentId == id && e.InstructorId == instructorID && e.CourseId == courseID);

                if (enrolled != null)
                {
                    _context.Enrolleds.Remove(enrolled);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine("------------exception-start-------");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("------------exception--end----------");
                ViewData["Error"] = "a problem has occurred, please try again";

                return View();
            }


        }

    }
}