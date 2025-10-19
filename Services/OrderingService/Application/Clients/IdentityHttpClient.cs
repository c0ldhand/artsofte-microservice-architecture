
using Application.DTO;
using CoreLib.HttpLogic.Services;
using CoreLib.HttpLogic.Services.Interfaces;


namespace Application.Clients
{
    internal class IdentityHttpClient : IIdentityClient
    {
        private readonly HttpConnectionData _conn;
        private readonly IHttpRequestService _http;

        public IdentityHttpClient(IHttpRequestService http)
        {
            _http = http;
            _conn = new HttpConnectionData("identity-api");
        }
        public async Task<UserDTO?> GetUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var req = new HttpRequestData
            {
                Method = HttpMethod.Get,
                Uri = new Uri($"http://identityservice-api/api/users/{id}")
            };

            var resp = await _http.SendRequestAsync<UserDTO>(req, _conn with { CancellationToken = ct });

            return resp.IsSuccessStatusCode ? resp.Body : null;


        }
    }
}
