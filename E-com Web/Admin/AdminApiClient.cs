using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace E_com_Web.Admin
{
    public class AdminApiClient
    {
        private readonly HttpClient _http;

        public AdminApiClient(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("AdminApi");
        }

        public Task<HttpResponseMessage> GetHealthAsync(CancellationToken cancellationToken = default)
        {
            return _http.GetAsync("/health", cancellationToken);
        }
    }
}
