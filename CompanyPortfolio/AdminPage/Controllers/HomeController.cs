using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            // Örnek: ViewBag kullanarak View'a gönder
            ViewBag.UserId = userId;

            return View();
        }
    }
}
