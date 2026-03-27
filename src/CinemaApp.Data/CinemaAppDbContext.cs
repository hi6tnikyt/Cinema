namespace CinemaApp.Data
{
    using CinemaApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class CinemaAppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public CinemaAppDbContext(DbContextOptions<CinemaAppDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<UserMovie> UsersMovies { get; set; } = null!;
        public virtual DbSet<Cinema> Cinemas { get; set; } = null!;
        public virtual DbSet<Projection> Projections { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. ВИНАГИ ПЪРВО ВИКАМЕ БАЗАТА ЗА IDENTITY
            base.OnModelCreating(modelBuilder);

            // 2. ТИПОВЕ ДАННИ ЗА ГРЕШКАТА, КОЯТО ИМАШЕ
            modelBuilder.Entity<Ticket>()
                .Property(t => t.UserId)
                .HasColumnType("uniqueidentifier");

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Id)
                .HasColumnType("uniqueidentifier");

            // 3. СЪСТАВЕН КЛЮЧ ЗА UserMovie (Many-to-Many)
            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });

            // 4. ТУК ТРЯБВА ДА СЛОЖИШ ТВОЯ SEEDING И ОСТАНАЛИТЕ КОНФИГУРАЦИИ
            // (Ако ги имаш в отделни файлове чрез ApplyConfigurationsFromAssembly, извикай ги тук)
        }
    }
}
