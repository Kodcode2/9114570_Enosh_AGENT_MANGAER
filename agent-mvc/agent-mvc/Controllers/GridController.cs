using agent_mvc.Services;
using agent_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace agent_mvc.Controllers
{
    public class GridController(IGridService gridService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await gridService.GetAllAgentsAndTargetsAsync());
        }
    }
}
