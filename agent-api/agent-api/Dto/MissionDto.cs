using agent_api.Model;

namespace agent_api.Dto
{
    public class MissionDto
    {
        public long MissionId { get; set; }
        public long AgentId { get; set; }
        public long TargetId { get; set; }

        public double MissionTime { get; set; }

        public MissionStatus MissionStatus { get; set; } = MissionStatus.IntialContract;

        public DateTime MissionCompletedTime { get; set; }

        
    }
}
