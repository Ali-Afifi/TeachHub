using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using online_course_platform.Data;
using online_course_platform.Models;
using online_course_platform.Utilities;

namespace online_course_platform.Controllers
{
    public class LoginController : Controller
    {

        private readonly OnlineCoursesContext _context;

        public LoginController(OnlineCoursesContext context)
        {
            _context = context;
        }

        // GET
        // Login/
        public IActionResult Index()
        {

            return View();
        }


        // POST
        // Login/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection collection)
        {

            var userName = collection["UserName"].ToString();
            var password = collection["Password"].ToString();

            if (userName == "" || password == "")
            {
                ViewData["Error"] = "Please fill the whole form";
                return View(nameof(Index));
            }



            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == userName);

            if (user == null)
            {
                ViewData["Error"] = "Wrong Credentials";
                return View(nameof(Index));
            }

            System.Console.WriteLine($"ID      : {user.Id}");
            System.Console.WriteLine($"username: {user.UserName}");
            System.Console.WriteLine($"password: {user.PasswordHash}");

            if (user.PasswordHash != password)
            {
                ViewData["Error"] = "Wrong Credentials";
                return View(nameof(Index));
            }


            var role = await _context.Roles.FirstOrDefaultAsync(m => m.UserId == user.Id);


            if (role == null)
            {
                ViewData["Error"] = "Something went wrong. Please try again.";
                return View(nameof(Index));
            }


            var token = Authentication.GenerateJwtToken(user.Id, userName, role.Role1);


            HttpContext.Session.SetString("_Token", token);

            return Redirect("/");
        }
    }
}