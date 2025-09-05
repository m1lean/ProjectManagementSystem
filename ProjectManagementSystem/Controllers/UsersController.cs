using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Participants(int projectId)
        {
            var participants = await _userService.GetUsersByProjectIdAsync(projectId) ?? new List<ProjectManagementSystem.Models.User>();
            ViewBag.ProjectId = projectId;
            ViewBag.AllUsers = await _userService.GetAllUsersAsync() ?? new List<ProjectManagementSystem.Models.User>();
            return View(participants);
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(int projectId, int userId)
        {
            await _userService.AddUserToProjectAsync(userId, projectId);
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveParticipant(int projectId, int userId)
        {
            await _userService.RemoveUserFromProjectAsync(userId, projectId);
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }
    }
}