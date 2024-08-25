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


        public string AgentNickName { get; set; }
        public string AgentPicture { get; set; }
        public AgentStatus AgentStatus { get; set; } = AgentStatus.SleepingCell;
        public int agentX { get; set; }
        public int agentY { get; set; }

        public string TargetName { get; set; }
        public string TargetRole { get; set; }
        public string TargetPicture { get; set; }
        public TargetStatus TargetStatus { get; set; } = TargetStatus.Alive;
        public int TargetX { get; set; }
        public int TargetY { get; set; }
    }
}
