using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agent_mvc.Model
{
    public enum TargetStatus
    {
        Alive,
        Targeted,
        Eliminated
    }
    public class TargetModel
    {
       
        public long TargetId { get; set; }
        public required string TargetName { get; set; }
        public required string TargetRole { get; set; }
        public required string TargetPicture { get; set; }
        public TargetStatus TargetStatus { get; set; } = TargetStatus.Alive;
        public long TargetLocationId { get; set; }
        public LocationModel TargetLocation { get; set; } = new LocationModel();
        public List<MissionModel> Missions { get; set; } = [];

    }
}
