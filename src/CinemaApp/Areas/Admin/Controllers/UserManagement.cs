using CinemaApp.Data.Seeding;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Areas.Admin.Controllers
{
    public class UserManagement : BaseAdminController
    {
        private readonly IUserService userService;
        public UserManagement(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            string? userId = this.GetAdminUserId()!;
            IEnumerable<UserManagerAll> userManage = await userService
                .GetAllManageableUsersAsync(userId);

            ViewData["AllRoles"] = IdentitySeeder.ApplicationRoles;
            return View(userManage);
        }
    }
}
