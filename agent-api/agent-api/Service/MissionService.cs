using agent_api.Data;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.DistanceUtils;
using static agent_api.Utils.Validations;
using static agent_api.Utils.MissionUtils;
using static agent_api.Utils.TimeUtils;
using System.Reflection;
using agent_api.Dto;
namespace agent_api.Service
{
    public class MissionService(ApplicationDBContext dBContext) : IMissionService
    {

        async Task UpdateMission(MissionModel mission)
        {
           LocationModel moveToLocation = FastestRouteToTarget(mission.Agent.AgentLocation, mission.Target.TargetLocation);          
            mission.Agent.AgentLocation = moveToLocation;
            mission.MissionTime = RemainingTimeFromMissionModel(mission);
            if (IsSameLocation(moveToLocation, mission.Target.TargetLocation))
            {                          
                mission.MissionStatus = MissionStatus.Completed;
                mission.Target.TargetStatus = TargetStatus.Eliminated;
                mission.Agent.AgentStatus = AgentStatus.SleepingCell;

            }
            dBContext.Missions.Update(mission);
            await dBContext.SaveChangesAsync();

        }
       
   
        
        bool MissionIsUnique(MissionModel mission)
            =>  !dBContext.Missions.Any(m => m.AgentId == mission.AgentId && m.TargetId == mission.TargetId);
            
            
       async Task RemoveNonValidMissions()
        {           
            var missionsThatAgentIsBusy = dBContext.Missions
                .Where(mission => mission.MissionStatus == MissionStatus.IntialContract &&
                  mission.Agent.AgentStatus == AgentStatus.ActiveCell).AsEnumerable();

            var missionsThatTargetIaBusy = dBContext.Missions
                .Where(mission => mission.MissionStatus == MissionStatus.IntialContract
                && mission.Target.TargetStatus == TargetStatus.Targeted); 
                                                         
            dBContext.RemoveRange(missionsThatAgentIsBusy);
            dBContext.RemoveRange(missionsThatTargetIaBusy);
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
                List<MissionModel> uniqueMissions = missions.Where(MissionIsUnique).ToList();
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
                List<MissionModel> uniqueMissions = missions.Where(MissionIsUnique).ToList();
                await AddMissionsAsync(uniqueMissions);
            }
        }

        public async Task AssignMissionAsync(long Missionid)
        {
            MissionModel mission = await dBContext.Missions
                .Include(m => m.Agent)
                .ThenInclude(agent => agent.AgentLocation)
                .Include(m => m.Target)
                .ThenInclude(target => target.TargetLocation)
                .FirstOrDefaultAsync(m => m.MissionId == Missionid)
                ?? throw new Exception($"mission by id{Missionid} not found");

            if (IsMissionValid(mission))
            {              
                mission.MissionStatus = MissionStatus.InProgress;
                mission.Agent.AgentStatus = AgentStatus.ActiveCell;
                mission.Target.TargetStatus = TargetStatus.Targeted;  
                mission.MissionTime = RemainingTimeFromMissionModel(mission);
                await dBContext.SaveChangesAsync();
            }
        }

        public async Task UpdateMissionsAsync()
        {
            await RemoveNonValidMissions();
            var missions = await dBContext.Missions
                .Where(mission => mission.MissionStatus == MissionStatus.InProgress)
                .Include(mission => mission.Agent)
                .ThenInclude(agent => agent.AgentLocation)
                .Include(mission => mission.Target)
                .ThenInclude(target => target.TargetLocation)
                .ToListAsync();
            foreach (var mission in missions)
            {
                await UpdateMission(mission);
            }

        }
        public async Task<List<MissionDto>> GetAllMissionsAsync()
        {
            var missionModels = await dBContext.Missions                  
                    .ToListAsync();
            List<MissionDto> missionDtos = missionModels.Select(MissionModelToMissionDto).ToList();
            return missionDtos;
        }
        async Task AddMissionsAsync(List<MissionModel> missions)
        {
           
            await dBContext.AddRangeAsync(missions);
            await dBContext.SaveChangesAsync();
        }
    }
}

