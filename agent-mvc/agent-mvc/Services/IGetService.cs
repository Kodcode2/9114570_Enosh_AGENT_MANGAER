namespace agent_mvc.Services
{
    public interface IGetService
    {
        Task<List<T>>  GetAllInfoAsync<T>(string modelType);
    }
}
