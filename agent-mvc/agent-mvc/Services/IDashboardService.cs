using agent_mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace agent_mvc.Services
{
    public interface IDashboardService 
    {
      Task<(List<AgentVM>, List<TargetVm>, List<MissionVM>)>  AllDashboardInfo();
        Task<List<AgentVM>> AllAgentInfo();
        Task<List<TargetVm>> AllTargetInfo();
        Task<List<MissionVM>> AllMissionInfo();

        Task<List<MissionVM>> AllMIssionsAvailableForAssignment();

    }
}
