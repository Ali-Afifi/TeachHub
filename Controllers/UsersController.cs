using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using online_course_platform.Data;
using online_course_platform.Models;

namespace online_course_platform.Controllers
{
    public class UsersController : Controller
    {
        private readonly OnlineCoursesContext _context;

        public UsersController(OnlineCoursesContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {

            var users = await _context.Users.ToListAsync();

            var roles = await _context.Roles.ToListAsync();

            var usersInfo = (from user in users
                             join role in roles on user.Id equals role.UserId
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

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null || role == null)
            {
                return NotFound();
            }


            dynamic model = new ExpandoObject();

            model.User = user;
            model.Role = role;

            return View(model);

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
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {

                var userInfo = new
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    BirthDate = collection["BirthDate"],
                    Gender = collection["Gender"],
                    Role = collection["Role"],
                    UserName = collection["UserName"],
                    Password = collection["Password"]
                };



                User user = new()
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    BirthDate = DateTime.Parse(userInfo.BirthDate),
                    Gender = true && userInfo.Gender == "Male",
                    UserName = userInfo.UserName,
                    PasswordHash = userInfo.Password
                };



                _context.Add(user);
                await _context.SaveChangesAsync();


                Role role = new()
                {
                    UserId = user.Id,
                    Role1 = userInfo.Role,
                };


                _context.Add(role);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {
                System.Console.WriteLine("------------exception-start-------");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("------------exception--end----------");
                ViewData["Error"] = "Please fill the whole form";

                return View();
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            var role = await _context.Roles.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null || role == null)
            {
                return NotFound();
            }

            dynamic model = new ExpandoObject();

            model.User = user;
            model.Role = role;

            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {


            try
            {

                var userInfo = new
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    BirthDate = collection["BirthDate"],
                    Gender = collection["Gender"],
                    Role = collection["Role"],
                    UserName = collection["UserName"],
                    Password = collection["Password"]
                };



                User user = new()
                {
                    Id = id,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    BirthDate = DateTime.Parse(userInfo.BirthDate),
                    Gender = true && userInfo.Gender == "Male",
                    UserName = userInfo.UserName,
                    PasswordHash = userInfo.Password
                };



                _context.Update(user);
                await _context.SaveChangesAsync();


                var role = await _context.Roles.FirstOrDefaultAsync(m => m.UserId == id);

                role.Role1 = userInfo.Role;


                _context.Update(role);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    System.Console.WriteLine("------------db-exception-start-------");
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine("------------db-exception--end----------");
                    ViewData["Error"] = "a problem has occurred, please try again";

                    return View();

                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("------------exception-start-------");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine("------------exception--end----------");
                ViewData["Error"] = "Please fill the whole form";

                return View();
            }

        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null || role == null)
            {
                return NotFound();
            }


            dynamic model = new ExpandoObject();

            model.User = user;
            model.Role = role;

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'OnlineCoursesContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);
            var role = await _context.Roles.FirstOrDefaultAsync(m => m.UserId == id);

            if (user != null && role != null)
            {
                _context.Roles.Remove(role);
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
