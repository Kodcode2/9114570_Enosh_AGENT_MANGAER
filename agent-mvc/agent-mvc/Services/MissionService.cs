using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class MissionService(IHttpClientFactory clientFactory) : IMissionService
    {
        string baseUrl = "https://localhost:7275/View/Missions";

        public async Task<List<MissionVM>> GetAllMissions()
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl);          
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
               string content = await response.Content.ReadAsStringAsync();
                List<MissionVM> missions = JsonSerializer.Deserialize<List<MissionVM>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return missions;
            }
            throw new Exception("error");
        }
    }
}
