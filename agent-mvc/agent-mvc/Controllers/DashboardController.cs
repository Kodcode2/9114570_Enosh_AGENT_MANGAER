using agent_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace agent_mvc.Controllers
{
    public class DashboardController(IDashboardService dashboardService) : Controller
    {
        public async Task<IActionResult> Index()
          => View(await dashboardService.AllDashboardInfo());
        
        public async Task<IActionResult> Agents()
          => View(await dashboardService.AllAgentInfo()); 
        public async Task<IActionResult> Targets()
          => View(await dashboardService.AllTargetInfo());
        public async Task<IActionResult> Missions()
          => View(await dashboardService.AllDashboardInfo());
        



    }
}
