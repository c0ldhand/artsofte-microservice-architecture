using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositories;
using IdentityService.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace IdentityService.Dal
{
    public class StartupDal
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

        }
    }
}
