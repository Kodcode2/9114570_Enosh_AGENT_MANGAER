using agent_api.Dto;
using agent_api.Model;

namespace agent_api.Service
{
    public interface ITargetInterface
    {
        Task<List<TargetDto>> GetAllTargetsAsync();
        Task<TargetDto> CreateTargetAsync(TargetDto targetDto);

        Task PinTargetAsync(PinLocationDto pinLocation, long id);

    }
}
