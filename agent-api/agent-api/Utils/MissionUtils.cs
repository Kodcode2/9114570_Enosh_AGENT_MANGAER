using agent_api.Model;
using static agent_api.Utils.DistanceUtils;
namespace agent_api.Utils
{
    public class MissionUtils
    {
        public static Func<AgentModel, TargetModel, bool> AreInDistanceForMission =
           (agent, target) => IsDistanceLessThan200KM(agent.AgentLocation, target.TargetLocation);


        public static Func<AgentModel, TargetModel, MissionModel> CreateMissionModel =
            (agent, target) => new()
            {
                AgentId = agent.AgentId,
                TargetId = target.TargetId,
            };

       public static Func<List<AgentModel>, TargetModel, List<MissionModel>> CreateListOfMissionModelsWithOneTarget =
            (agents, target) => agents.Select(agent => CreateMissionModel(agent, target)).ToList();

       public static Func<List<TargetModel>, AgentModel, List<MissionModel>> CreateListOfMissionModelsWithOneAgent =
            (targets, agent) => targets.Select(target => CreateMissionModel(agent, target)).ToList();

        static Func<MissionModel, MissionModel, bool> HasSameTargetId =
            (mission1, mission2) => mission1.TargetId == mission2.TargetId;
        static Func<MissionModel, MissionModel, bool> HasSameAgentId =
            (mission1, mission2) => mission1.AgentId == mission2.AgentId;
       public static Func<MissionModel, MissionModel, bool> IsSameMission =
            (mission1, mission2) => HasSameTargetId(mission1, mission2) && HasSameAgentId(mission1, mission2);


        static Func<int, int, int> MovmentBetweenPoints =
            (AgentPoint, TargetPoint) => AgentPoint == TargetPoint 
            ? AgentPoint 
            : AgentPoint > TargetPoint 
            ? AgentPoint -1 
            : AgentPoint +1;

        public static Func<LocationModel, LocationModel, LocationModel> FastestRouteToTarget =
            (agentLocation, targetLocation) => new()
            {
                x = MovmentBetweenPoints(agentLocation.x, targetLocation.x),
                y = MovmentBetweenPoints(agentLocation.y, targetLocation.y)
            };

        public static Func<LocationModel, LocationModel, bool> IsSameLocation =
            (location1, location2) => location1.y == location2.y && location1.x == location2.x;

    }
}
