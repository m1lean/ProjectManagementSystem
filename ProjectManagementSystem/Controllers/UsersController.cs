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

        // List all users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // Details
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // Create user
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

        // Edit user
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

        // Delete user
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

        // Participants for a project (view)
        public async Task<IActionResult> Participants(int projectId)
        {
            var participants = await _userService.GetParticipantsByProjectIdAsync(projectId);
            ViewBag.ProjectId = projectId;
            ViewBag.AllUsers = await _userService.GetAllUsersAsync(); // For add dropdown
            return View(participants);
        }

        // Add participant
        [HttpPost]
        public async Task<IActionResult> AddParticipant(int projectId, int userId)
        {
            await _userService.AddParticipantToProjectAsync(projectId, userId);
            return RedirectToAction("Participants", new { projectId });
        }

        // Remove participant
        [HttpPost]
        public async Task<IActionResult> RemoveParticipant(int projectId, int userId)
        {
            await _userService.RemoveParticipantFromProjectAsync(projectId, userId);
            return RedirectToAction("Participants", new { projectId });
        }
    }
}