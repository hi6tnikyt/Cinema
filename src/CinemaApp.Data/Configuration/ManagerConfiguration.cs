
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        void IEntityTypeConfiguration<Manager>.Configure(EntityTypeBuilder<Manager> entity)
        {
           entity
                .HasOne(m => m.User)
               .WithOne()
               .HasForeignKey<Manager>(m => m.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
