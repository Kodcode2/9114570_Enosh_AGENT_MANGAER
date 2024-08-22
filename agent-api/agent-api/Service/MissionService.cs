using agent_api.Data;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.DistanceUtils;
namespace agent_api.Service
{
    public class MissionService(ApplicationDBContext dBContext) : IMissionService
    {

        static Func<AgentModel, TargetModel, bool> AreInDistanceForMission =
            (agent, target) => IsDistanceLessThan200KM(agent.AgentLocation, target.TargetLocation);

        //static Func<List<AgentModel>, TargetModel, List<AgentModel>> GetAllAgentsWithin200KMOfTarget =
        //    (agents, target) => agents.Where(agent => AreInDistanceForMission(agent, target)).ToList();

        //static Func<List<TargetModel>, AgentModel, List<TargetModel>> GetAllTargetsWithin200KMOfAgent =
        //    (targets, agent) => targets.Where(target =>  AreInDistanceForMission(agent, target)).ToList();

        static Func<AgentModel, TargetModel, MissionModel> CreateMissionModel =
            (agent, target) => new()
            {
                AgentId = agent.AgentId,
                TargetId = target.TargetId,            
            };

        static Func<List<AgentModel>, TargetModel, List<MissionModel>> CreateListOfMissionModelsWithOneTarget =
            (agents, target) => agents.Select(agent => CreateMissionModel(agent, target)).ToList();

        static Func<List<TargetModel>, AgentModel, List<MissionModel>> CreateListOfMissionModelsWithOneAgent =
            (targets, agent) => targets.Select(target => CreateMissionModel(agent, target)).ToList();

        
        async Task AddMissionsAsync(List<MissionModel> missions)
        {
            await dBContext.AddRangeAsync(missions);
            await dBContext.SaveChangesAsync();
        }




        public async Task CreateMissionsAsync(AgentModel agent)
        {
             List<TargetModel> targets = await dBContext.Targets
                .Where(target => AreInDistanceForMission(agent, target))
                .ToListAsync();
            List<MissionModel> missions = CreateListOfMissionModelsWithOneAgent(targets, agent);
            await AddMissionsAsync(missions);

        }

        public async Task CreateMissionsAsync(TargetModel target)
        {
            List<AgentModel> agents = await dBContext.Agents
                .Where(agent =>  AreInDistanceForMission(agent, target))
                .ToListAsync();
            List<MissionModel> missions = CreateListOfMissionModelsWithOneTarget(agents, target);
            await AddMissionsAsync(missions);
        }
    }
}
