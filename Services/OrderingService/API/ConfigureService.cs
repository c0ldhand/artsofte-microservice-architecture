using Application;
using Infrastructure;

namespace API
{
    public class ConfigureService
    {
        public static void Configure(IServiceCollection services)
        {
            StartupApplication.Configure(services);
            StartupInfrastructure.Configure(services);
        }
    }
}
