namespace agent_api.Service
{
    public class LoginService(IJwtService jwtService) : ILoginService
    {
        static List<string> AuthorizedServers = ["SimulationServer", "MvcServer"];
        public string Login(string serverName)
        {
            if (AuthorizedServers.Contains(serverName))
            {
                return jwtService.GenerateToken(serverName);

            }
            throw new Exception("This server is unauthorized");

        }
        
    }
}
