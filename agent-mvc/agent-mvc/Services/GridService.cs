using agent_mvc.Model;
using agent_mvc.Models;
using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class GridService(IGetService getService) : IGridService
    {

        public async Task<(List<AgentVM>, List<TargetVm>)> GetAllAgentsAndTargetsAsync()
        { 
            var agents = await getService.GetAllInfoAsync<AgentVM>("Agents");
            var targets = await getService.GetAllInfoAsync<TargetVm>("Targets");
            return (agents, targets);
        }


        
      


    }
}
