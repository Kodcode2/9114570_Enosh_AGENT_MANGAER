using agent_api.Model;

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

        static Predicate<MissionModel> IsTargetAlive =
           (mission) => mission.Target.TargetStatus == TargetStatus.Alive;
        static Predicate<MissionModel> IsAgentAvailable =
            (mission) => mission.Agent.AgentStatus == AgentStatus.SleepingCell;
       

        public static Predicate<MissionModel> IsTargetAvailbleForMission =
          (target) => Validator<MissionModel>.Of(target)
          .Validate(IsTargetAlive)
          .Validate(IsTargetAvailbleForMission)
          .IsValid;

    }
}









