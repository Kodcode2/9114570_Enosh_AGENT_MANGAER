using agent_mvc.Dto;
using agent_mvc.Models;
using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class LoginService(IHttpClientFactory clientFactory) : ILoginService
    {
        string baseUrl = "https://localhost:7275/Login/";


        public async Task<string> LoginAsync(LoginDto login)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Post, baseUrl);
            var httpBody = JsonContent.Create(login);
            httpRequest.Content = httpBody;
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                TokenDto token = JsonSerializer.Deserialize<TokenDto>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return token.token;
            }
            throw new Exception($"could not login");
        } 

       


    }
}
