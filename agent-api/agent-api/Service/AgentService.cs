using agent_api.Data;
using agent_api.Dto;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.AgentUtils;
using static agent_api.Utils.LocationUtils;
namespace agent_api.Service
{
    public class AgentService(ApplicationDBContext dBContext) : IAgentService
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
            AgentModel agent = await GetAgentByIdAsync(id);
            var newLocation = UpdateLocation(agent.AgentLocation, direction);
            await SetAgentLocation(newLocation, id);
        }


        private async Task<AgentModel> GetAgentByIdAsync(long id)
            => await dBContext.Agents
                .Include(t => t.AgentLocation)
                .FirstOrDefaultAsync(t => t.AgentId == id)
                ?? throw new Exception($"Agent by id:{id} not found ");


        private async Task SetAgentLocation(LocationDto Location, long id)
        {
            if (IsLocationLegal(Location))
            {

                AgentModel agentToSet = await GetAgentByIdAsync(id);

                agentToSet.AgentLocation.x = Location.x;
                agentToSet.AgentLocation.y = Location.y;
                await dBContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("cannot set agent at elegal location");
            }
        }


        public async Task PinAgentLocationAsync(LocationDto pinLocation, long id)
        {
            await SetAgentLocation(pinLocation, id);
        }
    }
}
