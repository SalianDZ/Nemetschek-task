using Hierarchy.Data;
using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Project;

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

        public async Task<IEnumerable<ProjectListViewModel>> GetAllProjectsAsync()
        {
            var projects = await projectRepository.GetAllProjectsWithEmployeesAsync();

            return projects.Select(p => new ProjectListViewModel
            {
                ProjectID = p.,
                Name = p.Name,
                EmployeeCount = p.Employees?.Count() ?? 0
            }).ToList();
        }

        public Task<ProjectDetailViewModel> GetProjectDetailsAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }
    }
}
