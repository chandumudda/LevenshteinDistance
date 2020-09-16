using Microsoft.AspNetCore.Mvc;

namespace LD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
