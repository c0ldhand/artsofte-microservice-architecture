using CoreLib.HttpLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.HttpLogic.Services
{
    public record struct HttpConnectionData
    {
        public HttpConnectionData(string? clientName = null)
        {
            ClientName = clientName;
            Timeout = null;
            CancellationToken = default;
        }

        public TimeSpan? Timeout { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public string? ClientName { get; set; }
    }

    internal class HttpConnectionService : IHttpConnectionService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpConnectionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc />
        public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
        {
            var httpClient = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
                ? _httpClientFactory.CreateClient()
                : _httpClientFactory.CreateClient(httpConnectionData.ClientName);

            if (httpConnectionData.Timeout != null) httpClient.Timeout = httpConnectionData.Timeout.Value;

            return httpClient;
        }

        /// <inheritdoc />
        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage,
            HttpClient httpClient, CancellationToken cancellationToken,
            HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            var response = await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
            return response;
        }
    }
}
