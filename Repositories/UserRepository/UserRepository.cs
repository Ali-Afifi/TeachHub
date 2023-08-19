using online_course_platform.Models;
using online_course_platform.Data;
using Microsoft.EntityFrameworkCore;

namespace online_course_platform.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CoursesSystemContext _context;

        public UserRepository(CoursesSystemContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<User>?> GetAll()
        {
            try
            {
                return await _context.Users.Include(e => e.Enrollments)
                                           .Include(e => e.Roles)
                                           .Include(e => e.Teach)
                                           .ToListAsync();
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"DB Error: {e.Message}");
                return null;
            }

            // return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            try
            {
                return await _context.Users.Include(e => e.Enrollments)
                                           .Include(e => e.Roles)
                                           .Include(e => e.Teach)
                                           .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"DB Error: {e.Message}");
                return null;
            }
        }

        public async Task<User?> GetByUserName(string userName)
        {
            try
            {
                return await _context.Users.Include(e => e.Enrollments)
                                           .Include(e => e.Roles)
                                           .Include(e => e.Teach)
                                           .FirstOrDefaultAsync(e => e.UserName == userName);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"DB Error: {e.Message}");
                return null;
            }
        }

        public async Task<bool> Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }
        }
    }
}
