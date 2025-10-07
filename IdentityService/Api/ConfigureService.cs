using IdentityService.Dal;
using IdentityService.Logic;

namespace IdentityService.Api
{
    public class ConfigureService
    {
        public static void Configure(IServiceCollection services)
        {
            StartupLogic.Configure(services);
            StartupDal.Configure(services);
        }
    }
}
