using Application.Mapper;
using Application.Service.Abstract;
using Application.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Registry
{
    public static class ServiceRegistry
    {
        #region Methods
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Registering Services
            services.AddTransient<IBlockedEmailService, BlockedEmailService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IMailQueue, MailQueue>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IUserService, UserService>();

            // Add AutoMapper to services
            services.AddAutoMapper(typeof(BaseMapper));

            // Return the service collection
            return services;
        }
        #endregion
    }
}