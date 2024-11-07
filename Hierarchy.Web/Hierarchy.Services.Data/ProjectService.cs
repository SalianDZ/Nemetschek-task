using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Employee;
using Hierarchy.Web.Models.Project;
using Microsoft.Identity.Client;

namespace Hierarchy.Services.Data
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task AddProjectAsync(ProjectFormViewModel model)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = DateTime.UtcNow,
                EndDate = null
            };

            await projectRepository.AddProjectAsync(project);
        }

        public async Task<bool> DoesProjectExistAsync(string name)
        {
            return await projectRepository.DoesProjectExistAsync(name);
        }

        public async Task<bool> DoesProjectExistByIdAsync(string id)
        {
            return await projectRepository.DoesProjectExistByIdAsync(id);
        }

        public async Task<IEnumerable<ProjectListViewModel>> GetAllProjectsAsync()
        {
            var projects = await projectRepository.GetAllProjectsWithEmployeesAsync();

            return projects.Select(p => new ProjectListViewModel
            {
                ProjectID = p.Id,
                Name = p.Name,
                EmployeeCount = p.EmployeeProjects?.Count() ?? 0
            }).ToList();
        }

        public async Task<ProjectDetailViewModel> GetProjectDetailsAsync(Guid projectId)
        {
            var project = await projectRepository.GetProjectWithEmployeesByIdAsync(projectId);

            if (project == null) return null;

            return new ProjectDetailViewModel
            {
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Employees = project.EmployeeProjects?.Select(ep => new EmployeeViewModel
                {
                    EmployeeName = ep.Employee.Name,
                    Position = ep.Employee.Position?.Name
                }).ToList()
            };
        }
    }
}
