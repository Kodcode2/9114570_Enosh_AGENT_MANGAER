using agent_mvc.Model;
using agent_mvc.ViewModels;
namespace agent_mvc.ViewModels
{
    public class MissionVM
    {
        public long MissionId { get; set; }
        public long AgentId { get; set; }
        public long TargetId { get; set; }

        public double MissionTime { get; set; }

        public MissionStatus MissionStatus { get; set; } = MissionStatus.IntialContract;

        public DateTime MissionCompletedTime { get; set; }

        
        
    }
}
