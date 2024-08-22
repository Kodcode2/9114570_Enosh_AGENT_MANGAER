using agent_api.Model;

namespace agent_api.Dto
{
    public class AgentDto
    {
        public long AgentId { get; set; }
        public required string AgentNickName { get; set; }
        public required string AgentPicture { get; set; }
        public AgentStatus AgentStatus { get; set; } = AgentStatus.SleepingCell;
        public LocationModel AgentLocation { get; set; } = new LocationModel();
    }
}
