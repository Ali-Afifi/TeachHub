using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_course_platform.Data;

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
    }
}