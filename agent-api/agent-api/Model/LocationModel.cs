using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent_api.Model
{
    
    public class LocationModel
    {
        
        public long Id { get; set; }
        public int x { get; set; } = -1;
        public int y { get; set; } = -1;

    }
}
