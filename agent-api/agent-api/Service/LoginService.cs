namespace agent_api.Service
{
    public class LoginService(IJwtService jwtService) : ILoginService
    {
        static List<String> AutherisedServers = ["SimulationServer", "MvcServer"];
        public string Login(string serverName)
        {
            if (AutherisedServers.Contains(serverName))
            {
                return jwtService.GenerateToken(serverName);

            }
            throw new Exception("This server is unautherised");

        }

    }
}
