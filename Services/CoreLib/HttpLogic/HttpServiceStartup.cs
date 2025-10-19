using CoreLib.HttpLogic.Services;
using CoreLib.HttpLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace CoreLib.HttpLogic
{
    public static class HttpServiceStartup
    {
        public static IServiceCollection AddHttpRequestService(this IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddTransient<IHttpConnectionService, HttpConnectionService>();

            services.TryAddTransient<IHttpRequestService, HttpRequestService>();

            return services;
        }

    }
}
