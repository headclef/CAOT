using Domain.Repository.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Respository.Concrete;

namespace Persistence.Registry
{
    public static class RepositoryRegistry
    {
        #region Methods
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            // Registering Repositories
            repositories.AddTransient<IUserRepository, UserRepository>();
            repositories.AddTransient<IBlockedEmailRepository, BlockedEmailRepository>();

            // Registering UnitOfWork
            repositories.AddTransient<IUnitOfWork, UnitOfWork>();

            // Return the service collection
            return repositories;
        }
        #endregion
    }
}