using agent_mvc.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class GetService(IHttpClientFactory clientFactory, Authentication auth) : IGetService
    {
        string baseUrl = "https://localhost:7275/View/";

        public async Task<List<T>> GetAllInfoAsync<T>(string infoType)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl + infoType);
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.token);
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<T> info = JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return info;
            }
            throw new Exception($"could not fetch information of type {infoType}");
        }
    }
}
