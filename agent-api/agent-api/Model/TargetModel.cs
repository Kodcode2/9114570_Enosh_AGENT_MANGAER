using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agent_api.Model
{
    public enum TargetStatus
    {
        Alive,
        Eliminated
    }
    public class TargetModel
    {
        [Key]
        public long TargetId { get; set; }
        public required string TargetName { get; set; }
        public required string TargetRole { get; set; }
        public required string TargetPicture { get; set; }
        public TargetStatus TargetStatus { get; set; } = TargetStatus.Alive;
        public long TargetLocationId { get; set; }
        public LocationModel TargetLocation { get; set; } = new LocationModel();      
        public MissionModel Mission { get; set; }

    }
}
