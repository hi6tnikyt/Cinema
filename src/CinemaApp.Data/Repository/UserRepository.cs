
using System.Linq.Expressions;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private  readonly UserManager<ApplicationUser> userManager;
        private  readonly RoleManager<IdentityRole<Guid>> roleManager;
        public UserRepository(CinemaAppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
            : base(dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Expression<Func<ApplicationUser, bool>>? filterQuery = null, Expression<Func<ApplicationUser, ApplicationUser>>? projectionQuery = null)
        {
            IQueryable<ApplicationUser> applicationUsers = DbContext
                .Users
                .AsNoTracking();



            if (filterQuery != null)
            { 
             applicationUsers = applicationUsers
                    .Where(filterQuery);
            }

            if (projectionQuery != null)
            {
                applicationUsers = applicationUsers
                       .Select(projectionQuery);
            }


              IEnumerable<ApplicationUser> appUsers = await applicationUsers
                .OrderBy(u => u.Email)
                .ToArrayAsync();

            return appUsers;
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Expression<Func<ApplicationUser, bool>>? filterQuery = null, Expression<Func<ApplicationUser, ApplicationUser>>? projectionQuery = null, bool includeRoles = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser appUser)
        {
           IEnumerable<string> userRoles = await userManager.GetRolesAsync(appUser);

            return userRoles;
        }
    }
}
