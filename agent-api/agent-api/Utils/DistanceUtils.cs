using agent_api.Model;

namespace agent_api.Utils
{
    public class DistanceUtils
    {
        static Func<(int x, int y), (int x, int y), double> CalculateDistanceBetweenTwoPoints =
            (point1, point2) => Math.Sqrt(Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.y - point2.y, 2));

        static Func<LocationModel, LocationModel, double> CalculateDistanceBetweenTwoLocationModels =
            (model1, model2) => CalculateDistanceBetweenTwoPoints((model1.x, model1.y), (model2.x, model2.x));

        static Func<LocationModel, LocationModel, bool> IsDistanceLessThan200KM =
            (location1, location2) => CalculateDistanceBetweenTwoLocationModels(location1, location2) < 200;



    }
}
