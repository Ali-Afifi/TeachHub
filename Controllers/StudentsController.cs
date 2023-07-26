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
        // /Students
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
    }
}