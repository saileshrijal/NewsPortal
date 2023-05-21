using Microsoft.AspNetCore.Mvc;

namespace NewsPortal.Area.cms.Controllers
{
    [Area("cms")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}