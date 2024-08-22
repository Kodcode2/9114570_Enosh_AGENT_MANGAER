using agent_api.Data;
using agent_api.Dto;
using agent_api.Model;
using Microsoft.EntityFrameworkCore;

namespace agent_api.Service
{
    public class TargetService(ApplicationDBContext dBContext) : ITargetInterface
    {

        static Func<TargetDto, TargetModel> TargetDtoToTargetModel =
            (dto) => new() { 
            TargetName = dto.Name,
            TargetRole = dto.notes,
            TargetPicture = dto.Image
            };

        static Func<TargetModel, TargetDto> TargetModelToTargetDto =
            (model) => new()
            {
                TargetId = model.TargetId,
                TargetStatus = model.TargetStatus,
                Image = model.TargetPicture,
                Name = model.TargetName,    
                notes = model.TargetRole
            };




        public async Task<TargetDto> CreateTargetAsync(TargetDto targetDto)
        {
         TargetModel targetModelToAdd = TargetDtoToTargetModel(targetDto);
         await dBContext.Targets.AddAsync(targetModelToAdd);
         await dBContext.SaveChangesAsync();
         TargetDto  newTargetDto = TargetModelToTargetDto(targetModelToAdd);
         return newTargetDto;
         
            
        }

        public async Task<List<TargetDto>> GetAllTargetsAsync()
        {
           List<TargetModel> targets = await dBContext.Targets.ToListAsync();
           return targets.Select(TargetModelToTargetDto).ToList();
        }  
        
    }
}
