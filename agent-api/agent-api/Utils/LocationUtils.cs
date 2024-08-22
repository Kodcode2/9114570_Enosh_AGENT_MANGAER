using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Utils
{
    public class LocationUtils
    {

        static Predicate<int> IsInRange1000 =
            (num) => num >= 1 && num <= 1000;

        public static Predicate<PinLocationDto> IsLocationLegal =
            (location) => IsInRange1000(location.x) && IsInRange1000(location.y);   
            


    }
}
