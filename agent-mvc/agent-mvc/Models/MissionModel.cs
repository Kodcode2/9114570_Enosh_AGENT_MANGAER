using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agent_mvc.Model
{
    public enum MissionStatus
    {
        IntialContract,
        InProgress,
        Completed

    }
    public class MissionModel
    {
        
        public long MissionId { get; set; }
        public long AgentId { get; set; }
        public long TargetId { get; set; }

        public double MissionTime { get; set; }

        public MissionStatus MissionStatus { get; set; } = MissionStatus.IntialContract;

        public DateTime MissionCompletedTime { get; set; }

        public AgentModel Agent {  get; set; }

        public TargetModel Target { get; set; }

    }
}
