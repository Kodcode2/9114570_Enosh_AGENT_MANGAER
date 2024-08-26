using agent_api.Model;
using static agent_api.Utils.DistanceUtils;
namespace agent_api.Utils
{
    public class TimeUtils
    {

        public static Func<MissionModel, double> RemainingTimeFromMissionModel =
          (mission)  => MissionDistance(mission) / 5;
    }
}
