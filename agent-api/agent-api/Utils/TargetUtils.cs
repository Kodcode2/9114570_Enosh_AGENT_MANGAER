using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Utils
{
    public class TargetUtils
    {
         

        public static Func<TargetDto, TargetModel> TargetDtoToTargetModel =
            (dto) => new()
            {
                TargetId = dto.TargetId,
                TargetName = dto.Name,
                TargetRole = dto.notes,
                TargetPicture = dto.Image,
                TargetStatus = dto.TargetStatus,
                TargetLocation = dto.TargetLocation
            };

        public static Func<TargetModel, TargetDto> TargetModelToTargetDto =
            (model) => new()
            {
                TargetId = model.TargetId,
                TargetStatus = model.TargetStatus,
                Image = model.TargetPicture,
                Name = model.TargetName,
                notes = model.TargetRole,
                TargetLocation = model.TargetLocation
            };



    }
}
