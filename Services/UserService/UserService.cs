using online_course_platform.Models;
using online_course_platform.Data;

namespace online_course_platform.Services
{
    public class UserService : IUserService
    {
        // private readonly IUserRepository _userRepository;
        public void Add(User user)
        {
            throw new NotImplementedException();
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