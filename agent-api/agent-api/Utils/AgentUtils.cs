using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Utils
{
    public class AgentUtils
    {

        public static Func<AgentDto, AgentModel> AgentDtoToAgentModel =
           (dto) => new()
           {
            AgentId = dto.AgentId,
            AgentNickName = dto.AgentNickName,
            AgentPicture = dto.AgentPicture,
            AgentLocation = dto.AgentLocation,
            AgentStatus = dto.AgentStatus
               
           };

        public static Func<AgentModel, AgentDto> AgentModelToAgentDto =
            (model) => new()
            {
                AgentId = model.AgentId,
                AgentNickName = model.AgentNickName,
                AgentPicture = model.AgentPicture,
                AgentLocation = model.AgentLocation,
                AgentStatus = model.AgentStatus

            };

    }
}
