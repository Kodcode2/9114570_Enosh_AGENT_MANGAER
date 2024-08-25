using Microsoft.AspNetCore.Mvc;

namespace agent_mvc.Controllers
{
    public class GridController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
