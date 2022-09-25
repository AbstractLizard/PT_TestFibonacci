namespace FirstService.Services.Implementation
{
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <inheritdoc cref="ISenderService"/>>
    public class SenderService : ISenderService
    {
        public SenderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;

        public async Task<HttpResponseMessage> Send<T>(string url, T obj)
        {
            var serializeMsg = new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            return await _httpClient.PostAsync(url, serializeMsg).ConfigureAwait(false);
        }
    }
}