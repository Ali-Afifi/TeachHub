using online_course_platform.Models;

namespace online_course_platform.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByUserName(string userName);
        Task<IEnumerable<User>> GetAll();
        Task<bool> Create(User user);
        bool Update(User user);
        bool Delete(User user);

    }
}