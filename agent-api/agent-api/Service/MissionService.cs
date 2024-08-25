using agent_api.Data;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.DistanceUtils;
using static agent_api.Utils.Validations;
using static agent_api.Utils.MissionUtils;
namespace agent_api.Service
{
    public class MissionService(ApplicationDBContext dBContext) : IMissionService
    {

        async Task UpdateMission(MissionModel mission)
        {
           LocationModel moveToLocation = FastestRouteToTarget(mission.Agent.AgentLocation, mission.Target.TargetLocation);          
            mission.Agent.AgentLocation = moveToLocation;
            if (IsSameLocation(moveToLocation, mission.Target.TargetLocation))
            {                          
                mission.MissionStatus = MissionStatus.Completed;
                mission.Target.TargetStatus = TargetStatus.Eliminated;
                mission.Agent.AgentStatus = AgentStatus.SleepingCell;

            }
            dBContext.Missions.Update(mission);
            await dBContext.SaveChangesAsync();

        }
       

        

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

        public async Task AssignMissionAsync(long Missionid)
        {
            MissionModel mission = await dBContext.Missions.Include(m => m.Agent).Include(m => m.Target)
                .FirstOrDefaultAsync(m => m.MissionId == Missionid)
                ?? throw new Exception($"mission by id{Missionid} not found");

            if (IsMissionValid(mission))
            {              
                mission.MissionStatus = MissionStatus.InProgress;
                mission.Agent.AgentStatus = AgentStatus.ActiveCell;
                mission.Target.TargetStatus = TargetStatus.Targeted;             
                await dBContext.SaveChangesAsync();
            }
        }

        public async Task UpdateMissionsAsync()
        {
            
            var missions = await dBContext.Missions
                .Where(mission => mission.MissionStatus == MissionStatus.InProgress)
                .Include(mission => mission.Agent)
                .ThenInclude(agent => agent.AgentLocation)
                .Include(mission => mission.Target)
                .ThenInclude(target => target.TargetLocation)
                .ToListAsync();
            var tasks = missions.Select(UpdateMission);
            await Task.WhenAll(tasks);

        }
    }
}

