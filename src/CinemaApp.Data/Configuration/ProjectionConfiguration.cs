using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class ProjectionConfiguration : IEntityTypeConfiguration<Projection>
    {
        public void Configure(EntityTypeBuilder<Projection> entity)
        {
            entity
                 .HasIndex(cm => new { cm.CinemaId, cm.MovieId, cm.Showtime }, "IX_CinemaMovie_Mapping_Unique")
                 .IsUnique();

            entity
                 .HasQueryFilter(p => p.Movie.IsDeleted == false && p.Cinema.IsDeleted == false);

            entity.HasData(this.SeedProjections());
        }

        private IEnumerable<Projection> SeedProjections()
        {
            return new List<Projection>
            {
                new Projection
                {
                    Id = Guid.NewGuid(), 
                    CinemaId = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), 
                    MovieId = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                    Showtime = new TimeOnly(18, 30),
                    AvailableTickets = 120,
                    TicketPrice = 12.50m,
                    IsDeleted = false
                },
                new Projection
                {
                    Id = Guid.NewGuid(),
                    CinemaId = Guid.Parse("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"), 
                    MovieId = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                    Showtime = new TimeOnly(20, 00),
                    AvailableTickets = 200,
                    TicketPrice = 14.00m,
                    IsDeleted = false
                },
                new Projection
                {
                    Id = Guid.NewGuid(),
                    CinemaId = Guid.Parse("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"), 
                    MovieId = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                    Showtime = new TimeOnly(16, 45),
                    AvailableTickets = 80,
                    TicketPrice = 10.00m,
                    IsDeleted = false
                }
            };
        }
    }
}