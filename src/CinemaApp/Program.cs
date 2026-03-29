

namespace CinemaApp.Web
{
    using CinemaApp.Web.Infrastructure.Extensions;
    using CinemaApp.Data;
    using CinemaApp.Data.Repository;
    using CinemaApp.Services.Core;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Seeding;
    using CinemaApp.Data.Seeding.Contracts;
    using CinemaApp.Web.Infrastructure.Utilities.Contracts;
    using CinemaApp.Web.Infrastructure.Utilities;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration
                .GetConnectionString("SqlServerDev") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<CinemaAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.RegisterRepositories(typeof(MovieRepository));

            builder.Services.RegisterUserServices(typeof(MovieService));

            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            builder.Services.AddSingleton<ISlugGenerator, SlugGenerator>();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
            {
                ConfigureIdentity(builder.Configuration, options);
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<CinemaAppDbContext>();

            builder.Services.AddCors(config =>
            {
                config.AddPolicy("AllowMvcDomain", policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins("https://localhost:7180")
                    .WithMethods("GET", "POST")
                    .AllowAnyHeader();
                });
            });
            

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowMvcDomain");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRolesSeeder();
            app.UseAdminUserSeeder();

            app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?code={0}");

            app.MapControllerRoute(
                name: "adminArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
            name: "cinemaProgram",
            pattern: "Cinema/Program/{slug}/{id}",
            defaults: new { controller = "Cinema", action = "Program" });
            app.MapControllerRoute(
                name: "slugRoute",
                pattern: "{controller=Home}/{action=Index}/{slug:required}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
        private static void ConfigureIdentity(ConfigurationManager configuration, IdentityOptions options)
        {
            options.Password.RequireDigit =
                configuration.GetValue<bool>("Identity:Password:RequireDigit");

            options.Password.RequiredLength =
                configuration.GetValue<int>("Identity:Password:RequiredLength");

            options.Password.RequiredUniqueChars =
                configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");

            options.Password.RequireNonAlphanumeric =
                configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");

            options.Password.RequireUppercase =
                configuration.GetValue<bool>("Identity:Password:RequireUppercase");

            options.Password.RequireLowercase =
                configuration.GetValue<bool>("Identity:Password:RequireLowercase");

            options.SignIn.RequireConfirmedEmail =
                configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");

            options.SignIn.RequireConfirmedPhoneNumber =
                configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            options.SignIn.RequireConfirmedAccount =
                configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
        }
    }

}
