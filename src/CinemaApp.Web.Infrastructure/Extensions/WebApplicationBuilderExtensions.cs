using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaApp.Web.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection, Type repositoryType)
        { 
         Assembly repositoriesAssembly = repositoryType.Assembly;
            IEnumerable<Type> repositoryInterfaces = repositoriesAssembly
                .GetTypes()
                .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.EndsWith("Repository"))
                .ToArray();
            foreach (Type serviceType in repositoryInterfaces)
            {
                Type implementationType = repositoriesAssembly
                    .GetTypes()
                    .Single(t => t is{IsClass: true, IsAbstract: false } && serviceType.IsAssignableFrom(t));

                serviceCollection.AddScoped(serviceType, implementationType);
            }
            return serviceCollection;
        }
    }
}
