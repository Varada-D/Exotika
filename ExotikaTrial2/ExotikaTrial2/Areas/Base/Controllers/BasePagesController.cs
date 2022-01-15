using Microsoft.AspNetCore.Mvc;

namespace ExotikaTrial2.Controllers
{
    [Area("Base")]
    public class BasePagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
