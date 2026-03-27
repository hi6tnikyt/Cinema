
using CinemaApp.Web.ViewModels.ApplicationUser;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserManagerAll>> GetAllManageableUsersAsync(string adminUserId);
    }
}
