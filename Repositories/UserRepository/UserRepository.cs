using online_course_platform.Models;
using online_course_platform.Data;

namespace online_course_platform.Repositories {
    public class UserRepository : IUserRepository
    {
        private readonly CoursesSystemContext _context;

        public UserRepository(CoursesSystemContext context){
            _context = context;
        }

        public async void Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();    
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public User Update(int id, User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
