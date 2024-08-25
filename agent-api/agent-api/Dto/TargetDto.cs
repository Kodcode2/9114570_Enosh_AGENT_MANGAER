using agent_api.Model;

namespace agent_api.Dto
{
    public class TargetDto
    {
        public long TargetId { get; set; }
        public string Name { get; set; }
        public string notes { get; set; }
        public string Image {  get; set; }
        public TargetStatus TargetStatus { get; set; } = TargetStatus.Alive;
        public LocationModel TargetLocation { get; set; } = new LocationModel();


    }
}
