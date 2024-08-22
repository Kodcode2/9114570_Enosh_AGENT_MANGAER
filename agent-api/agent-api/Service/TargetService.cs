using agent_api.Data;
using agent_api.Dto;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;
using static agent_api.Utils.ConvertUtils;
using static agent_api.Utils.LocationUtils;


namespace agent_api.Service
{
    public class TargetService(ApplicationDBContext dBContext) : ITargetInterface
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

        public async Task PinTargetAsync(PinLocationDto pinLocation, long id)
        {
            if (IsLocationLegal(pinLocation))
            {

                TargetModel targetToPin = await dBContext.Targets
                    .Include(target => target.TargetLocation)
                    .FirstOrDefaultAsync(target => target.TargetId == id)
                    ?? throw new Exception($"Target by id:{id} not found ");

                targetToPin.TargetLocation.x = pinLocation.x;
                targetToPin.TargetLocation.y = pinLocation.y;
                await dBContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("cannot pin user at elegal location");
            }
        }
    }
}
