

using System.Linq.Expressions;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IProjectionRepository
    {
        Task<IEnumerable<Projection>> GetProjectionsAsync(Expression<Func<Projection, bool>>? filterQuery = null);

        Task<Projection?> FindByIdAsync(Guid id);

        Task<bool> EditProjectionAsync(Projection projection);
    }
}
