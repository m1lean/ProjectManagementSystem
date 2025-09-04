using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}