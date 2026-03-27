
using System.Linq.Expressions;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IUserRepository
    {
         Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Expression<Func<ApplicationUser, bool>>? filterQuery = null,
             Expression<Func<ApplicationUser, ApplicationUser>>? projectionQuery = null);

        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser appUser);
    }
}
