using agent_mvc.Model;

namespace agent_mvc.ViewModels
{
    public class AgentVM
    {
        public long Id { get; set; }
        public required string nickname { get; set; }
        public required string photoUrl { get; set; }
        public AgentStatus AgentStatus { get; set; } = AgentStatus.SleepingCell;
        public LocationModel AgentLocation { get; set; } = new LocationModel();
        public int TotalKills { get; set; }
    }
}
