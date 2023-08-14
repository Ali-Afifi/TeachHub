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

        public async Task<bool> Add(User user)
        {
            await _context.Users.AddAsync(user);
            return true;
        }

        public bool Delete(User user)
        {
            _context.Users.Remove(user);
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(e => e.Enrollments)
                                       .Include(e => e.Roles)
                                       .Include(e => e.Teach)
                                       .ToListAsync();

            // return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.Include(e => e.Enrollments)
                                       .Include(e => e.Roles)
                                       .Include(e => e.Teach)
                                       .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await _context.Users.Include(e => e.Enrollments)
                                       .Include(e => e.Roles)
                                       .Include(e => e.Teach)
                                       .FirstOrDefaultAsync(e => e.UserName == userName);
        }

        public bool Update(User user)
        {
            _context.Users.Update(user);
            return true;
        }
    }
}
