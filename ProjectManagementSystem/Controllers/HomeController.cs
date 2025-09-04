using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Services;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public HomeController(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProjectCount = (await _projectService.GetAllProjectsAsync()).Count();
            ViewBag.UserCount = (await _userService.GetAllUsersAsync()).Count();
            return View();
        }
    }
}