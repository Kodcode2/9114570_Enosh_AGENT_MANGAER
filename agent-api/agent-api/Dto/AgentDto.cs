using agent_api.Model;

namespace agent_api.Dto
{
    public class AgentDto
    {
        
        public long Id { get; set; }
        public required string nickname { get; set; }
        public required string photoUrl { get; set; }
        public AgentStatus AgentStatus { get; set; } = AgentStatus.SleepingCell;
        public LocationModel AgentLocation { get; set; } = new LocationModel();
    }
}
