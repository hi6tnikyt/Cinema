

using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class CinemaConfigurations : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> entity)
        {
            entity.HasQueryFilter(c => c.IsDeleted == false);
            entity.HasData(SeedCinemas());  
        }


            private IEnumerable<Cinema> SeedCinemas()
        { 
         return new List<Cinema>
            {
                new Cinema
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                    Name = "Downtown Cinema",
                    Location = "Main Street 123",
                    IsDeleted = false
                },
                new Cinema
                {
                    Id = Guid.Parse("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"),
                    Name = "City Center Multiplex",
                    Location = "Central Avenue 45",
                    IsDeleted = false
                },
                new Cinema
                {
                    Id = Guid.Parse("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"),
                    Name = "Riverside Screens",
                    Location = "River Road 7",
                    IsDeleted = false
                }
            };
        }
        
    }
}
