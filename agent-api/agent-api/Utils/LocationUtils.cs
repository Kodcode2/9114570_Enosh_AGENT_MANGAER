using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Utils
{
    public class LocationUtils
    {
        static Dictionary<string, (int x, int y)> CoordinatesDictionary = new()
        {
            {"n", (0, 1) },
            {"s", (0, -1) },
            {"e", (1, 0) },
            {"w", (-1, 0) },
            {"ne", (1, 1) },
            {"nw", (-1, 1) },
            {"se", (1, -1) },
            {"sw", (-1, -1) }

        };

        static Predicate<int> IsInRange1000 =
            (num) => num >= 1 && num <= 1000;

        public static Predicate<LocationDto> IsLocationLegal =
            (location) => IsInRange1000(location.x) && IsInRange1000(location.y);

        public static Func<DirectionDto, (int x, int y)> DirectionDtoToCoordinates =
            (dto) => CoordinatesDictionary.GetValueOrDefault(dto.direction);

        public static Func<LocationModel, (int x, int y), LocationDto> UpdateLocationAccordingToCoordinates =
            (location, coordinates) => new() { x = location.x + coordinates.x, y = location.y + coordinates.y };

        public static Func<LocationModel, DirectionDto, LocationDto> UpdateLocation =
            (location, direction) => UpdateLocationAccordingToCoordinates(location, DirectionDtoToCoordinates(direction));

    }
}
