using CoreLib.Interfaces.Services;
using IdentityService.Logic.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Logic
{
    public class StartupLogic
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

        }
    }
}
