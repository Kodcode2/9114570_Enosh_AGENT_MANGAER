using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Utils
{
    public class AgentUtils
    {

        public static Func<AgentDto, AgentModel> AgentDtoToAgentModel =
           (dto) => new()
           {
            AgentId = dto.Id,
            AgentNickName = dto.nickname,
            AgentPicture = dto.photoUrl,
            AgentLocation = dto.AgentLocation,
            AgentStatus = dto.AgentStatus
               
           };

        public static Func<AgentModel, AgentDto> AgentModelToAgentDto =
            (model) => new()
            {
                Id = model.AgentId,
                nickname = model.AgentNickName,
                photoUrl = model.AgentPicture,
                AgentLocation = model.AgentLocation,
                AgentStatus = model.AgentStatus,
                TotalKills = model.Missions.Where(m => m.MissionStatus == MissionStatus.Completed).Count()
                

            };

    }
}
