using agent_mvc.Models;
using agent_mvc.ViewModels;
using System.Text.Json;
using System.Net.Http.Headers;
namespace agent_mvc.Services
{
    public class DashboardService(IGetService getService) : IDashboardService
    {
 
        
        public async Task<List<AgentVM>> AllAgentInfo()
        => await getService.GetAllInfoAsync<AgentVM>("Agents");



        public async Task<List<MissionVM>> AllMissionInfo()
        => await getService.GetAllInfoAsync<MissionVM>("Missions");

        public Task<List<MissionVM>> AllMIssionsAvailableForAssignment()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TargetVm>> AllTargetInfo()
            => await getService.GetAllInfoAsync<TargetVm>("Targets");
        public async Task<(List<AgentVM>, List<TargetVm>, List<MissionVM>)> AllDashboardInfo()
        => (
            await AllAgentInfo(),
            await AllTargetInfo(), 
            await AllMissionInfo()
            );
    }
}
