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

        public static Predicate<TargetModel> IsTargetAvailable =
           (target) => target.TargetStatus == TargetStatus.Alive;
        public static Predicate<AgentModel> IsAgentAvailable =
            (agent) => agent.AgentStatus == AgentStatus.SleepingCell;
       

        public static Predicate<MissionModel> IsMissionValid =
          (mission) => Validator<MissionModel>.Of(mission)
          .Validate(mission => IsTargetAvailable(mission.Target))
          .Validate(mission => IsAgentAvailable(mission.Agent))
          .Validate(mission=> IsDistanceLessThan200KM(mission.Agent.AgentLocation, mission.Target.TargetLocation))
          .IsValid;

    }
}









