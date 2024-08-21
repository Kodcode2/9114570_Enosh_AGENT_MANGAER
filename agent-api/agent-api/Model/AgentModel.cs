using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agent_api.Model
{
    public enum AgentStatus
    {
        SleepingCell,
        ActiveCell
    }
    public class AgentModel
    {
        [Key]
        public long AgentId { get; set; }
        public required string AgentNickName { get; set; }
        public required string AgentPicture { get; set; }       
        public AgentStatus AgentStatus { get; set; } = AgentStatus.SleepingCell;
        public long AgentLocationId { get; set; }

        public  LocationModel AgentLocation { get; set; } = new LocationModel();

        public List<MissionModel> Missions { get; set; } = [];
    }
}
