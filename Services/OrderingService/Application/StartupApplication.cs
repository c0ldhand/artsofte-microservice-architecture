using Application.Clients;
using Application.Services;
using CoreLib.HttpLogic.Services;
using CoreLib.HttpLogic.Services.Interfaces;
using CoreLib.TraceIdLogic;
using CoreLib.TraceLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Application
{
    public class StartupApplication
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<OrderService>();
            services.AddHttpClient();
            services.AddTransient<IHttpConnectionService, HttpConnectionService>();
            services.TryAddTransient<IHttpRequestService, HttpRequestService>();
            services.AddScoped<TraceIdAccessor>();
            services.TryAddScoped<ITraceWriter>(p => p.GetRequiredService<TraceIdAccessor>());
            services.TryAddScoped<ITraceReader>(p => p.GetRequiredService<TraceIdAccessor>());
            services.TryAddScoped<ITraceIdAccessor>(p => p.GetRequiredService<TraceIdAccessor>());
            services.AddScoped<IIdentityClient, IdentityHttpClient>();

        }
    }
}
