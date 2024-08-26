using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class DashboardService(IHttpClientFactory clientFactory) : IDashboardService
    {

        string baseUrl = "https://localhost:7275/View/";

      
        async Task<List<T>> GetInfoAsync<T>(string infoType)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Get, baseUrl + infoType);
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<T> info = JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return info;
            }
            throw new Exception($"could not fetch information of type {infoType}");
        }
        public async Task<List<AgentVM>> AllAgentInfo()
        => await GetInfoAsync<AgentVM>("Agents");


        public async Task<(List<AgentVM>, List<TargetVm>, List<MissionVM>)> AllDashboardInfo()
        => (
            await AllAgentInfo(),
            await AllTargetInfo(), 
            await AllMissionInfo()
            );

        public async Task<List<MissionVM>> AllMissionInfo()
        => await GetInfoAsync<MissionVM>("Missions");

        public Task<List<MissionVM>> AllMIssionsAvailableForAssignment()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TargetVm>> AllTargetInfo()
            => await GetInfoAsync<TargetVm>("Targets");
    }
}
