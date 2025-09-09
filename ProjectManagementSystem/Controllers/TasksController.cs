using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TasksController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int projectId)
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public async Task<IActionResult> Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(task);
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            // repopulate select list when returning view due to validation errors
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name");
            ViewBag.ProjectId = task.ProjectId;
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name", task.AssignedUserId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTask task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name", task.AssignedUserId);
            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }
    }
}