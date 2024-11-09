using Hierarchy.Data.Models;
using Hierarchy.Services.Data;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Department;
using Hierarchy.Web.Models.Project;
using Microsoft.AspNetCore.Mvc;

namespace Hierarchy.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public async Task<IActionResult> All()
        {
            var projects = await projectService.GetAllProjectsAsync();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ProjectFormViewModel model = new ProjectFormViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectFormViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View(model);
            }

            if (await projectService.DoesProjectExistAsync(model.Name))
            {
                return NotFound("There is already a project with the same name!");
            }

            try
            {
                await projectService.AddProjectAsync(model);
                return RedirectToAction("All", "Project");
            }
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            bool isGuidCorrect = Guid.TryParse(id, out var projectId);

            if (!isGuidCorrect) 
            {
                return NotFound("There is no such a project!");
            }

            var project = await projectService.GetProjectDetailsAsync(Guid.Parse(id));

            if (project == null)
            {
                return NotFound("There is no such a project!");
            }
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            var project = await projectService.GetProjectDetailsAsync(correctId);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            try
            {
                await projectService.DeleteProjectAsync(correctId);
                return RedirectToAction("All", "Project");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Delete", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            try
            {
                ProjectEditViewModel model = await projectService.GetProjectForEditAsync(correctId);
                return View(model);
            }
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectEditViewModel model, string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            try
            {
                await projectService.UpdateProjectAsync(model, correctId);
                return RedirectToAction("All", "Project");
            }
            catch (Exception)
            {
                return NotFound("An error occured during the process of connecting to the database!");
            }
        }
    }
}
