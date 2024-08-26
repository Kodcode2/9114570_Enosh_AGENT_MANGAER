using agent_mvc.ViewModels;

namespace agent_mvc.Services
{
    public interface IGridService
    {
        Task<(List<AgentVM>, List<TargetVm>)> GetAllAgentsAndTargetsAsync();
    }
}
