using agent_api.Model;

namespace agent_api.Service
{
    public interface ITargetInterface
    {
        Task<List<TargetModel>> GetAllTargetsAsync();
        Task<TargetModel> CreateTargetAsync();


    }
}
