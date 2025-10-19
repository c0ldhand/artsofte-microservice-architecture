using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.HttpLogic.Services.Interfaces
{
    public interface IHttpConnectionService
    {
        HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

        Task<HttpResponseMessage> SendRequestAsync( HttpRequestMessage httpRequestMessage, HttpClient httpClient,CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
    }
}
