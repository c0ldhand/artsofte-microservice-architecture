using CoreLib.HttpLogic.Services;
using Microsoft.IdentityModel.Protocols;

namespace CoreLib.HttpLogic.Services.Interfaces
{
    public interface IHttpRequestService
    {
        /// <summary>
        /// Отправить HTTP-запрос
        /// </summary>
        Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default);
    }

}
