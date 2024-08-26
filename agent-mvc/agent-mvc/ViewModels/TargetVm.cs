using agent_mvc.Model;

namespace agent_mvc.ViewModels
{
    public class TargetVm
    {

        public long Id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string photoUrl { get; set; }
        public TargetStatus TargetStatus { get; set; } = TargetStatus.Alive;
        public LocationModel TargetLocation { get; set; } = new LocationModel();

    }
}
