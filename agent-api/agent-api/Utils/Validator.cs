using agent_api.Model;
using static agent_api.Utils.DistanceUtils;
namespace agent_api.Utils
{
    public class Validator<T>
    {

        private T Subject;
        public bool IsValid { get; } = true;
        private Validator(T subject, bool isValid)
        {
            Subject = subject;
            IsValid = isValid;
        }
        public static Validator<R> Of<R>(R subject) => new Validator<R>(subject, true);
        public Validator<T> Validate(Predicate<T> predicate) =>
            new(Subject, IsValid && predicate(Subject));
    }

    public class Validations
    {

        static Predicate<TargetModel> IsTargetStatusAvailable =
           (target) => target.TargetStatus == TargetStatus.Alive;
        static Predicate<AgentModel> IsAgentStatusAvailable =
            (agent) => agent.AgentStatus == AgentStatus.SleepingCell;
        static Predicate<int> IsNumPostive =
             (num) => num > 0;
        static Predicate<LocationModel> IsLocatonValid =
            (location) => IsNumPostive(location.x) && IsNumPostive(location.y);


        public static Predicate<MissionModel> IsMissionValid =
          (mission) => Validator<MissionModel>.Of(mission)
          .Validate(mission => IsTargetStatusAvailable(mission.Target))
          .Validate(mission => IsAgentStatusAvailable(mission.Agent))
          .Validate(mission=> IsDistanceLessThan200KM(mission.Agent.AgentLocation, mission.Target.TargetLocation))
          .IsValid;
        
        public static Predicate<AgentModel> IsAgentAvailable =
          (agent) => Validator<AgentModel>.Of(agent)
          .Validate(IsAgentStatusAvailable)
          .Validate(agent => IsLocatonValid(agent.AgentLocation))         
          .IsValid;

        public static Predicate<TargetModel> IsTargetAvailable =
          (target) => Validator<TargetModel>.Of(target)
          .Validate(IsTargetStatusAvailable)
          .Validate(target => IsLocatonValid(target.TargetLocation))         
          .IsValid;

    }
}









