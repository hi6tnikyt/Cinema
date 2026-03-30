
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.ApplicationUser;

namespace CinemaApp.Services.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IEnumerable<UserManagerAll>> GetAllManageableUsersAsync(string adminUserId)
        {
            var users = await userRepository
                .GetAllUsersAsync(u => u.Id.ToString() != adminUserId);

            var userViewModels = new List<UserManagerAll>();

            foreach (var user in users)
            {
                var roles = await userRepository.GetUserRolesAsync(user);

                userViewModels.Add(new UserManagerAll
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Roles = roles
                });
            }

            return userViewModels;
        }

    }
}
