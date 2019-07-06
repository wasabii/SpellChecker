using System.Net.Http;
using System.Threading.Tasks;

using SpellChecker.Contracts;

namespace SpellChecker.Core
{
    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _http;

        public HttpClient(System.Net.Http.HttpClient http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _http.GetAsync(requestUri);
        }
    }
}
