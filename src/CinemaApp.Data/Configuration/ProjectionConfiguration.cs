

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
                .HasIndex(cm => new { cm.CinemaId, cm.MovieId, cm.Showtime}, "IX_CinemaMovie_Mapping_ Unique")
                .IsUnique();

            entity
                .HasQueryFilter(p => p.Movie.IsDeleted == false && p.Cinema.IsDeleted == false);
        }
    }
}
