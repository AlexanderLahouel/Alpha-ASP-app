using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Alpha.Models;
using Alpha.Services;

namespace Alpha.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: /Project/Index
        public async Task<IActionResult> Index(ProjectStatus? status)
        {
            List<ProjectModel> projects = status switch
            {
                ProjectStatus.Started => await _projectService.GetByStatusAsync(ProjectStatus.Started),
                ProjectStatus.Completed => await _projectService.GetByStatusAsync(ProjectStatus.Completed),
                _ => await _projectService.GetAllAsync()
            };

            // Always return a list, even if it's empty, to prevent ViewData errors
            projects ??= new List<ProjectModel>();

            return View(projects);
        }

        // POST: /Project/Add
        [HttpPost]
        public async Task<IActionResult> Add(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.AddAsync(project);
                return RedirectToAction("Index");
            }

            // Optionally: Return a partial with validation errors if modal-based
            TempData["AddError"] = "Invalid form data.";
            return RedirectToAction("Index");
        }

        // GET: /Project/EditModal/5
        public async Task<IActionResult> EditModal(string id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return PartialView("_EditProjectModal", project);
        }

        // POST: /Project/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.UpdateAsync(project);
                return RedirectToAction("Index");
            }

            return PartialView("_EditProjectModal", project);
        }

        // POST: /Project/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _projectService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

