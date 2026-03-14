
namespace CinemaApp.Web.Infrastructure.Extensions
{
    public class EntityKeyNullOrEmptyException : Exception
    {
        public EntityKeyNullOrEmptyException()
        {
            
        }

        public EntityKeyNullOrEmptyException(string message)
            : base(message)
        {

        }
    }
}
