using CoreLib.TraceLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Context;


namespace CoreLib.TraceIdLogic
{
    public interface ITraceIdAccessor
    {
    }
    public static class StartUpTraceId
    {
        public static IServiceCollection TryAddTraceId(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<TraceIdAccessor>();
            serviceCollection
                .AddScoped<ITraceWriter>(provider => provider.GetRequiredService<TraceIdAccessor>());
            serviceCollection
                .AddScoped<ITraceReader>(provider => provider.GetRequiredService<TraceIdAccessor>());
            serviceCollection
                .AddScoped<ITraceIdAccessor>(provider => provider.GetRequiredService<TraceIdAccessor>());

            return serviceCollection;
        }
    }

    public class TraceIdAccessor : ITraceReader, ITraceWriter, ITraceIdAccessor
    {
        public string Name => "TraceId";

        private string _value;

        public string GetValue()
        {
            return _value;
        }

        public void WriteValue(string value)
        {
            // на случай если это первый в цепочке сервис и до этого не было traceId
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Guid.NewGuid().ToString();
            }

            _value = value;
            LogContext.PushProperty("TraceId", value);
        }
    }
}
