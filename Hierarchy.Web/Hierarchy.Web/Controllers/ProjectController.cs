using Hierarchy.Services.Data;
using Hierarchy.Services.Data.Interfaces;
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
                //We have to implement a better error handling method!
                return RedirectToAction("All", "Project");
            }

            try
            {
                await projectService.AddProjectAsync(model);
                return RedirectToAction("All", "Project");
            }
            catch (Exception)
            {
                //We have to implement a better error handling method!
                return RedirectToAction("All", "Project");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            var project = await projectService.GetProjectDetailsAsync(Guid.Parse(id));
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
    }
}
