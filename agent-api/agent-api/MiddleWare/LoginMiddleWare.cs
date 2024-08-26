using agent_api.Dto;
using Azure.Core;
using System.Text.Json;

namespace agent_api.MiddleWare
{
    public class LoginMiddleWare(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            var req = context.Request;
            if (req.Path != "/Login")
            {
                using (StreamReader reader = new StreamReader(req.Body))
                {
                    string requestBody = await reader.ReadToEndAsync();
                    TokenDto token = JsonSerializer.Deserialize<TokenDto>(requestBody, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                    Console.WriteLine(token.token);

                }
            }
            Console.WriteLine(req.Path.ToString());
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
    }
}
