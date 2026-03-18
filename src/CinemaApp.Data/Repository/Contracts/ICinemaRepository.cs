
using System.Linq.Expressions;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<Cinema>> GetAllCinemas(Expression<Func<Cinema, bool>>? filterQuery = null,
            Expression<Func<Cinema, Cinema>>?projectionQuery = null, bool includeProjections = false);
    }
}
