using agent_api.Data;
using agent_api.Dto;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;

namespace agent_api.Service
{
    public class TargetService(ApplicationDBContext dBContext) : ITargetInterface
    {

        static Func<TargetDto, TargetModel> TargetDtoToTargetModel =
            (dto) => new()
            {
                TargetId = dto.TargetId,
                TargetName = dto.Name,
                TargetRole = dto.notes,
                TargetPicture = dto.Image,
                TargetStatus = dto.TargetStatus,
                TargetLocation = dto.TargetLocation
            };

        static Func<TargetModel, TargetDto> TargetModelToTargetDto =
            (model) => new()
            {
                TargetId = model.TargetId,
                TargetStatus = model.TargetStatus,
                Image = model.TargetPicture,
                Name = model.TargetName,
                notes = model.TargetRole,
                TargetLocation = model.TargetLocation
            };




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
            TargetModel targetToPin = await dBContext.Targets
                .Include(target => target.TargetLocation)
                .FirstOrDefaultAsync(target => target.TargetId == id)
                ?? throw new Exception($"Target by id:{id} not found ");

            targetToPin.TargetLocation.x = pinLocation.x;
            targetToPin.TargetLocation.y = pinLocation.y;
            await dBContext.SaveChangesAsync();
                     
        }
    }
}
