using agent_mvc.Models;
using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class GridService(IHttpClientFactory clientFactory, Authentication auth) : IGridService
    {
        string baseUrl = "https://localhost:7275/View/";

        public async Task<(List<AgentVM>, List<TargetVm>)> GetAllAgentsAndTargetsAsync()
        { 
            var agents = await GetAllAgentsAsync();
            var targets = await GetAllTargetsAsync();
            return (agents, targets);
        }


        async Task<List<AgentVM>> GetAllAgentsAsync()
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl + "Agents");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.token);
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<AgentVM> agents = JsonSerializer.Deserialize<List<AgentVM>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return agents;
            }
            throw new Exception("error");
        }
         async Task<List<TargetVm>> GetAllTargetsAsync()
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl + "Targets");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.token);
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<TargetVm> targets = JsonSerializer.Deserialize<List<TargetVm>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return targets;
            }
            throw new Exception("error");
        }



    }
}
