using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Utils
{
    public class TargetUtils
    {
         

        public static Func<TargetDto, TargetModel> TargetDtoToTargetModel =
            (dto) => new()
            {
                TargetId = dto.Id,
                TargetName = dto.name,
                TargetRole = dto.position,
                TargetPicture = dto.photoUrl,
                TargetStatus = dto.TargetStatus,
                TargetLocation = dto.TargetLocation
            };

        public static Func<TargetModel, TargetDto> TargetModelToTargetDto =
            (model) => new()
            {
                Id = model.TargetId,
                TargetStatus = model.TargetStatus,
                photoUrl = model.TargetPicture,
                name = model.TargetName,
                position = model.TargetRole,
                TargetLocation = model.TargetLocation
            };

       

    }
}
