using Microsoft.AspNetCore.Mvc;

namespace NewsPortal.Areas.cms.Controllers
{
    [Area("cms")]
    public class AccountController : Controller
    {

        [HttpGet("cms/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}