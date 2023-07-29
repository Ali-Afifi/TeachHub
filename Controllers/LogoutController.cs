using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using online_course_platform.Data;
using online_course_platform.Models;
using online_course_platform.Utilities;
using online_course_platform.Filters;


namespace online_course_platform.Controllers
{
    [SessionCheck]
    public class LogoutController : Controller
    {
        
        [HttpGet]
        public IActionResult Index() {
            return Redirect("/");
        }
        
        
        // POST
        // Logout/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Logout")]
        public IActionResult Index(IFormCollection collection)
        {
            HttpContext.Session.SetString("_Token", "");
            return Redirect("/");
        }
    }
}