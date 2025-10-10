using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StartupInfrastructure
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(GenericRepository<>));
            services.AddScoped<OrderRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<UnitOfWork>();
        }
    }
}
