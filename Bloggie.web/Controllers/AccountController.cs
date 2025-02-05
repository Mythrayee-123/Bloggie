using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
