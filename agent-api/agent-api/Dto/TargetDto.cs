using agent_api.Model;

namespace agent_api.Dto
{
    public class TargetDto
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string photoUrl {  get; set; }
        public TargetStatus TargetStatus { get; set; } = TargetStatus.Alive;
        public LocationModel TargetLocation { get; set; } = new LocationModel();


    }
}
