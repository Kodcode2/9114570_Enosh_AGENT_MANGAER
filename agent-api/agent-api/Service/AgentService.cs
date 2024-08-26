using agent_api.Data;
using agent_api.Dto;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.AgentUtils;
using static agent_api.Utils.LocationUtils;
namespace agent_api.Service
{ 
    public class AgentService(ApplicationDBContext dBContext, IMissionService missionService) : IAgentService
    {
        public async Task<AgentDto> CreateAgentAsync(AgentDto targetDto)
        {
            AgentModel agentModelToAdd = AgentDtoToAgentModel(targetDto);
            await dBContext.Agents.AddAsync(agentModelToAdd);
            await dBContext.SaveChangesAsync();
            AgentDto newAgentDto = AgentModelToAgentDto(agentModelToAdd);
            return newAgentDto;
        }

        public async Task<List<AgentDto>> GetAllAgentsAsync()
        {
            List<AgentModel> agents = await dBContext.Agents.Include(target => target.AgentLocation).ToListAsync();
            return agents.Select(AgentModelToAgentDto).ToList();
        }

        public async Task MoveAgentLocationAsync(DirectionDto direction, long id)
        {
            try
            {
                AgentModel agentToMove = await GetAgentByIdAsync(id);
                var newLocation = UpdateLocation(agentToMove.AgentLocation, direction);
                await SetAgentLocation(newLocation, agentToMove);
            }
            catch (Exception ex)
            {
                throw  ex;
            }
            
        }


        private async Task<AgentModel> GetAgentByIdAsync(long id)
            => await dBContext.Agents
                .Include(t => t.AgentLocation)
                .FirstOrDefaultAsync(t => t.AgentId == id)
                ?? throw new Exception($"Agent by id:{id} not found ");


        private async Task SetAgentLocation(LocationDto location, AgentModel agentToSet)
        {
            if (IsLocationLegal(location))
            {


                agentToSet.AgentLocation.x = location.x;
                agentToSet.AgentLocation.y = location.y;
                await dBContext.SaveChangesAsync();
                await missionService.CreateMissionsAsync(agentToSet);
            }
            else
            {
                throw new Exception("cannot set agent at elegal location");
            }
        }


        public async Task PinAgentLocationAsync(LocationDto pinLocation, long id)
        {
            try
            {
                AgentModel agentToPin = await GetAgentByIdAsync(id);
                await SetAgentLocation(pinLocation, agentToPin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
