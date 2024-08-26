using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class MissionService(IHttpClientFactory clientFactory) : IMissionService
    {
        string baseUrl = "https://localhost:7275/Missions/";
            
        public  bool AssignMission(long id)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl + $"Assign/{id}");          
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            return response.IsSuccessStatusCode;
            
        }



    }
}
