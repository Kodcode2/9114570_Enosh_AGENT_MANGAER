using agent_mvc.Dto;
using agent_mvc.Models;
using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class MissionService(IHttpClientFactory clientFactory, Authentication auth) : IMissionService
    {
        string baseUrl = "https://localhost:7275/Missions/";
            
        public  bool AssignMission(long id)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl + $"Assign/{id}");   
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.token);

            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            return response.IsSuccessStatusCode;
            
        }

       



    }
}
