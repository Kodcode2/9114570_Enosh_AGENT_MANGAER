using agent_api.Data;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.DistanceUtils;
using static agent_api.Utils.Validations;
namespace agent_api.Service
{
    public class MissionService(ApplicationDBContext dBContext) : IMissionService
    {

        static Func<AgentModel, TargetModel, bool> AreInDistanceForMission =
            (agent, target) => IsDistanceLessThan200KM(agent.AgentLocation, target.TargetLocation);


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

        static Func<MissionModel, MissionModel, bool> HasSameTargetId =
            (mission1, mission2) => mission1.TargetId == mission2.TargetId;
        static Func<MissionModel, MissionModel, bool> HasSameAgentId =
            (mission1, mission2) => mission1.AgentId == mission2.AgentId;
        static Func<MissionModel, MissionModel, bool> IsSameMission =
            (mission1, mission2) => HasSameTargetId(mission1, mission2) && HasSameAgentId(mission1, mission2);

        // how do i add an await
        async Task<List<MissionModel>> RemoveDuplicateMissions(List<MissionModel> missions)
        {
            var a = await dBContext.Missions.ToListAsync();
            var b = missions.Where(m => !a.Any(s => IsSameMission(s, m))).ToList();
            return b;
        }



        async Task AddMissionsAsync(List<MissionModel> missions)
        {
           
            await dBContext.AddRangeAsync(missions);
            await dBContext.SaveChangesAsync();
        }




        public async Task CreateMissionsAsync(AgentModel agent)
        {
            if (IsAgentAvailable(agent))
            {
                List<TargetModel> targets = await dBContext.Targets
                    .Include(target => target.TargetLocation)
                    .ToListAsync();
                List<TargetModel> validTargets = targets.Where(target => IsTargetAvailable(target))
                    .Where(target => AreInDistanceForMission(agent, target))
                    .ToList();
                List<MissionModel> missions = CreateListOfMissionModelsWithOneAgent(validTargets, agent);
                List<MissionModel> uniqueMissions = await RemoveDuplicateMissions(missions);
                await AddMissionsAsync(uniqueMissions);
            }
            
        }

        public async Task CreateMissionsAsync(TargetModel target)
        {
            if (IsTargetAvailable(target))
            {
                List<AgentModel> agents = await dBContext.Agents
                    .Include(agent => agent.AgentLocation)
                    .ToListAsync();
                List<AgentModel> validAgents = agents.Where(agent => IsAgentAvailable(agent))
                    .Where(agent => AreInDistanceForMission(agent, target)).ToList();
                List<MissionModel> missions = CreateListOfMissionModelsWithOneTarget(agents, target);
                List<MissionModel> uniqueMissions = await RemoveDuplicateMissions(missions);
                await AddMissionsAsync(uniqueMissions);
            }
        }

        public async Task AssignMissionAsync(MissionModel mission)
        {
            if (IsMissionValid(mission))
            {
                MissionModel missionToAssign = await dBContext.Missions
                    .Include(m => m.Agent)
                    .ThenInclude(agent => agent.AgentLocation)
                    .Include(m => m.Target)
                    .ThenInclude(target => target.TargetLocation)
                    .FirstOrDefaultAsync(m => m.MissionId == mission.MissionId)
                    ?? throw new Exception($"could not find mission by mission id {mission.MissionId}");

                missionToAssign.MissionStatus = MissionStatus.InProgress;
                missionToAssign.Agent.AgentStatus = AgentStatus.ActiveCell;
                missionToAssign.Target.TargetStatus = TargetStatus.Targeted;
            }
        }
    }
}

