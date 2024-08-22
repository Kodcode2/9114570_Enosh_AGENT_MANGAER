using agent_api.Data;
using agent_api.Dto;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.TargetUtils;
using static agent_api.Utils.LocationUtils;


namespace agent_api.Service
{
    public class TargetService(ApplicationDBContext dBContext, IMissionService missionService) : ITargetInterface
    {

        

        public async Task<TargetDto> CreateTargetAsync(TargetDto targetDto)
        {
            TargetModel targetModelToAdd = TargetDtoToTargetModel(targetDto);
            await dBContext.Targets.AddAsync(targetModelToAdd);
            await dBContext.SaveChangesAsync();
            TargetDto newTargetDto = TargetModelToTargetDto(targetModelToAdd);
            return newTargetDto;
        }

        public async Task<List<TargetDto>> GetAllTargetsAsync()
        {
            List<TargetModel> targets = await dBContext.Targets.Include(target => target.TargetLocation).ToListAsync();
            return targets.Select(TargetModelToTargetDto).ToList();
        }

        public async Task MoveTargetLocationAsync(DirectionDto direction, long id)
        {
            TargetModel target = await GetTargetByIdAsync(id);
            var newLocation = UpdateLocation(target.TargetLocation, direction);
            await SetTargetLocation(newLocation, id);
        }


        private async Task<TargetModel> GetTargetByIdAsync(long id) 
            => await dBContext.Targets
                .Include(t => t.TargetLocation)
                .FirstOrDefaultAsync(t => t.TargetId == id)
                ?? throw new Exception($"Target by id:{id} not found ");


        private async Task SetTargetLocation(LocationDto Location, long id)
        {
            if (IsLocationLegal(Location))
            {

                TargetModel targetToPin = await GetTargetByIdAsync(id);
                targetToPin.TargetLocation.x = Location.x;
                targetToPin.TargetLocation.y = Location.y;
                await dBContext.SaveChangesAsync();
                await missionService.CreateMissionsAsync(targetToPin);
            }
            else
            {
                throw new Exception("cannot set target at elegal location");
            }
        }


        public async Task PinTargetLocationAsync(LocationDto pinLocation, long id)
        {
            await SetTargetLocation(pinLocation, id);
        }
    }
}
