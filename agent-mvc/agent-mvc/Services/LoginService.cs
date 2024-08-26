using agent_mvc.Dto;
using agent_mvc.Models;
using agent_mvc.ViewModels;
using System.Text.Json;

namespace agent_mvc.Services
{
    public class LoginService(IHttpClientFactory clientFactory, Authentication auth) : ILoginService
    {
        string baseUrl = "https://localhost:7275/Login/";


        public async Task LoginAsync()
        {
            LoginDto loginDto = new LoginDto() { id = "MvcServer" };

            HttpClient httpClient = clientFactory.CreateClient();
            HttpRequestMessage httpRequest = new(HttpMethod.Post, baseUrl);
            var httpBody = JsonContent.Create(loginDto);
            httpRequest.Content = httpBody;
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                TokenDto token = JsonSerializer.Deserialize<TokenDto>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                auth.token = token.token;
                return;
            }
            throw new Exception($"could not login");
        } 

       


    }
}
