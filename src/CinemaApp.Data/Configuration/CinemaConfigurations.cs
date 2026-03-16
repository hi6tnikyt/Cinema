

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
        }
    }
}
