using agent_mvc.Model;
using agent_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace agent_mvc.Controllers
{
    public class MissionController(IMissionService missionService) : Controller
    {
        public async Task<IActionResult> Index() 
            => View(await missionService.GetAllMissions());
        
           

            
        
    }
}
