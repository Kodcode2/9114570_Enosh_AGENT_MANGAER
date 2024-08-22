using agent_api.Dto;

namespace agent_api.Service
{
    public interface IAgentService
    {
        Task<List<AgentDto>> GetAllAgentsAsync();
        Task<AgentDto> CreateAgentAsync(AgentDto targetDto);
        Task PinAgentLocationAsync(LocationDto pinLocation, long id);
        Task MoveAgentLocationAsync(DirectionDto direction, long id);


    }
}
