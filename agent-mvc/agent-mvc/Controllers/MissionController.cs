using agent_mvc.Model;
using agent_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace agent_mvc.Controllers
{
    public class MissionController(IMissionService missionService) : Controller
    {
       public  IActionResult AssignMission(long id)
       => missionService.AssignMission(id) 
            ? RedirectToAction("Missions", "Dashboard") 
            : RedirectToAction("Index", "Home");

        
           

            
        
    }
}
