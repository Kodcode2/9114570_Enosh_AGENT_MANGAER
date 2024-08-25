using agent_api.Model;

namespace agent_api.Service
{
    public interface IMissionService
    {
        Task CreateMissionsAsync(AgentModel agentModel);
        Task CreateMissionsAsync(TargetModel targetModel);
        Task UpdateMissionsAsync();
        Task AssignMissionAsync(long missionId);
    }
}
