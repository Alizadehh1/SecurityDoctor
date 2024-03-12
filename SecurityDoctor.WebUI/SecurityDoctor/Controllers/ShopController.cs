using Microsoft.AspNetCore.Mvc;

namespace SecurityDoctor.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
