
using CinemaApp.Data.Models;
using CinemaApp.Data.Seeding.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static CinemaApp.GCommon.ExceptionMessages;
namespace CinemaApp.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        public static string[] ApplicationRoles = new[]
            {
            "Admin",
            "Moderator",
            "User"
        };

        private readonly RoleManager<IdentityRole<Guid>> _roleManager;  
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._configuration = configuration;
        }
        public async Task SeedRolesAsync()
        {
            foreach (string role in ApplicationRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);

                    IdentityResult result = await _roleManager.CreateAsync(newRole);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(string.Format(RoleSeedingExceptionMessage, role));
                    }
                }
            }
        }


        public async Task SeedAdminUserAsync()
        {
            string adminEmail = _configuration["UserSeed:AdminAccount:Email"] ??
                throw new InvalidOperationException(AdminUserSeedingEmailNotFoundMessage);
            string adminPassword = _configuration["UserSeed:AdminAccount:Password"] ??
                throw new InvalidOperationException(AdminUserSeedingPasswordNotFoundMessage);

            ApplicationUser? adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Birthdate = DateTime.Now.AddYears(-18),
                    EmailConfirmed = true 
                };

                IdentityResult result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(AdminUserSeedingExceptionMessage);
                }
            }

            if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                IdentityResult result = await _userManager.AddToRoleAsync(adminUser, "Admin");
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(AdminUserSeedingExceptionMessage);
                }
            }
        }
    }
}
