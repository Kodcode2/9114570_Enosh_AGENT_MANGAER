using agent_mvc.ViewModels;

namespace agent_mvc.Services
{
    public interface IMissionService
    {
        Task<List<MissionVM>> GetAllMissions();
    }
}
