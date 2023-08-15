using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using online_course_platform.Data;
using online_course_platform.Models;
using online_course_platform.ViewModels;
using online_course_platform.Services;
using NuGet.Protocol.Plugins;
using System.Runtime.Intrinsics.Arm;

namespace online_course_platform.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly AuthenticationService _authenticationService;

        public UsersController(IUserService userService, AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll();

            if (users == null)
            {
                return Problem("users not found");
            }

            return View(users);

            //   return _context.Users != null ? 
            //               View(await _context.Users.ToListAsync()) :
            //               Problem("Entity set 'CoursesSystemContext.Users'  is null.");

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById((int)id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {


            if (form != null && !String.IsNullOrEmpty(form["UserName"]) && !String.IsNullOrEmpty(form["Password"]) && !String.IsNullOrEmpty(form["FirstName"]) && !String.IsNullOrEmpty(form["LastName"]) && !String.IsNullOrEmpty(form["Role"]) && !String.IsNullOrEmpty(form["Gender"]) && !String.IsNullOrEmpty(form["BirthDate"]))
            {

                UserViewModel user = new UserViewModel();
                user.UserName = form["UserName"];
                user.FirstName = form["FirstName"];
                user.LastName = form["LastName"];
                user.Gender = form["Gender"] == "Male" ? Gender.Male : Gender.Female;
                user.BirthDate = DateTime.Parse(form["BirthDate"]);

                Password password = _authenticationService.HashPassword(form["Password"]);

                user.PasswordHash = password.PasswordHash;
                user.PasswordHashSalt = password.Salt;

                bool isUserCreated = await _userService.Create(user);

                if (isUserCreated)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            ViewData["Error"] = "Please fill the whole form";

            return View(nameof(Create));
        }

        // GET: Users/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null || _context.Users == null)
        //     {
        //         return NotFound();
        //     }

        //     var user = await _context.Users.FindAsync(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(user);
        // }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,FirstName,LastName,Gender,BirthDate,PasswordHash,PasswordHashSalt,UpdateBy,IpAddressOfLastUpdate,UpdatedAt,LastUpdateOperation")] User user)
        // {
        //     if (id != user.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(user);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!UserExists(user.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(user);
        // }

        // GET: Users/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null || _context.Users == null)
        //     {
        //         return NotFound();
        //     }

        //     var user = await _context.Users
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(user);
        // }

        // POST: Users/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     if (_context.Users == null)
        //     {
        //         return Problem("Entity set 'CoursesSystemContext.Users'  is null.");
        //     }
        //     var user = await _context.Users.FindAsync(id);
        //     if (user != null)
        //     {
        //         _context.Users.Remove(user);
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool UserExists(int id)
        // {
        //     return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        // }
    }
}
