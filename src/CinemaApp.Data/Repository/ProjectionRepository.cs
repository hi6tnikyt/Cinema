
using System.Linq.Expressions;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class ProjectionRepository : BaseRepository, IProjectionRepository
    {
        public ProjectionRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<bool> EditProjectionAsync(Projection projection)
        {
            DbContext!.Projections.Update(projection);
            int resultCount =  await SaveChangeAsync();

            return resultCount == 1;
        }

        public async Task<Projection?> FindByIdAsync(Guid id)
        {
            return await DbContext!
                 .Projections
                 .FindAsync(id);

        }

        public async Task<IEnumerable<Projection>> GetProjectionsAsync(Expression<Func<Projection, bool>>? filterQuery = null)
        {
            IQueryable<Projection> projectionsFetchQuery = this.DbContext
                .Projections
                .AsNoTracking();
            if (filterQuery != null)
            {
                projectionsFetchQuery = projectionsFetchQuery.Where(filterQuery);
            }

            return await projectionsFetchQuery
                .ToArrayAsync();
        }
    }
}
