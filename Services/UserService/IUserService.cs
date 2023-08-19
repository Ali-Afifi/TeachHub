using online_course_platform.Models;
using online_course_platform.ViewModels;

namespace online_course_platform.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>?> GetAll();
        Task<UserViewModel?> GetById(int id);
        Task<UserViewModel?> GetByUserName(string userName);
        Task<bool> Create(UserViewModel userViewModel);
        Task<bool> Update(UserViewModel userViewModel);
        Task<bool> Delete(int id);

    }
}