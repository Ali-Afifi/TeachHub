using online_course_platform.Models;

namespace online_course_platform.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByUserName(string userName);
        Task<IEnumerable<User>?> GetAll();
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);

    }
}