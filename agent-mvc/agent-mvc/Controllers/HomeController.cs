using agent_mvc.Models;
using agent_mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using agent_mvc.Dto;

namespace agent_mvc.Controllers
{
    public class HomeController(ILoginService loginService, ILogger<HomeController> logger, Authentication token) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

       

        public async Task<IActionResult> Index()
        {
            LoginDto loginDto = new LoginDto() { id = "MvcServer"};
            token.token = await loginService.LoginAsync(loginDto);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
