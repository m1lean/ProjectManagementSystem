using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly IProjectTaskService _taskService;
        private readonly IUserService _userService;

        public TasksController(IProjectTaskService taskService, IUserService userService)
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
            if (task == null) return NotFound();
            return View(task);
        }

        public async Task<IActionResult> Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.Users = await _userService.GetAllUsersAsync();
            return View(new ProjectTask { ProjectId = projectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(task);
                return RedirectToAction("Index", new { projectId = task.ProjectId });
            }
            ViewBag.Users = await _userService.GetAllUsersAsync();
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            ViewBag.Users = await _userService.GetAllUsersAsync();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTask task)
        {
            if (id != task.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("Index", new { projectId = task.ProjectId });
            }
            ViewBag.Users = await _userService.GetAllUsersAsync();
            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
    }
}