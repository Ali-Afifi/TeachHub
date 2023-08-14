using online_course_platform.Models;
using online_course_platform.ViewModels;
using online_course_platform.Repositories;

namespace online_course_platform.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public bool Create(UserViewModel userViewModel)
        {

            User user = new User();
            user.UserName = userViewModel.UserName;
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.BirthDate = userViewModel.BirthDate;
            user.Gender = Convert.ToBoolean((int)userViewModel.Gender);

            if (String.IsNullOrEmpty(userViewModel.PasswordHash) || String.IsNullOrEmpty(userViewModel.PasswordHashSalt))
            {
                return false;
            }

            user.PasswordHash = userViewModel.PasswordHash;
            user.PasswordHashSalt = userViewModel.PasswordHashSalt;

            _userRepository.Create(user);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user != null)
            {
                return _userRepository.Delete(user);
            }

            return false;
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var users = await _userRepository.GetAll();

            List<UserViewModel> userViewModelGroup = new List<UserViewModel>();

            foreach (var user in users)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender ? Gender.Male : Gender.Female,
                    BirthDate = user.BirthDate,
                    Roles = user.Roles,
                    Enrollments = user.Enrollments,
                    Teach = user.Teach
                };

                userViewModelGroup.Add(userViewModel);
            }

            return userViewModelGroup;

        }

        public async Task<UserViewModel?> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user != null)
            {
                UserViewModel userViewModel = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender ? Gender.Male : Gender.Female,
                    BirthDate = user.BirthDate,
                    Roles = user.Roles,
                    Enrollments = user.Enrollments,
                    Teach = user.Teach
                };
                return userViewModel;
            }

            return null;

        }

        public async Task<UserViewModel?> GetByUserName(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);

            if (user != null)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender ? Gender.Male : Gender.Female,
                    BirthDate = user.BirthDate,
                    Roles = user.Roles,
                    Enrollments = user.Enrollments,
                    Teach = user.Teach
                };

                return userViewModel;

            }

            return null;

        }

        public bool Update(UserViewModel userViewModel)
        {
            User user = new User();
            user.Id = userViewModel.Id;
            user.UserName = userViewModel.UserName;
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Gender = Convert.ToBoolean((int)userViewModel.Gender);

            if (String.IsNullOrEmpty(userViewModel.PasswordHash) || String.IsNullOrEmpty(userViewModel.PasswordHashSalt))
            {
                return false;
            }

            user.PasswordHash = userViewModel.PasswordHash;
            user.PasswordHashSalt = userViewModel.PasswordHashSalt;

            return _userRepository.Update(user);

        }
    }

}