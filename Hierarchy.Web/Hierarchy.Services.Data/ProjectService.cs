using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Employee;
using Hierarchy.Web.Models.Project;

namespace Hierarchy.Services.Data
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IEmployeeProjectRepository employeeProjectRepository;
        public ProjectService(IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            this.projectRepository = projectRepository;
            this.employeeProjectRepository = employeeProjectRepository;
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

        public async Task AssignProjectToEmployeeAsync(Guid employeeId, Guid projectId)
        {
            await projectRepository.AssignProjectToEmployee(employeeId, projectId);
        }

        public async Task DeleteProjectAsync(Guid projectId)
        {
            // Check if there are any associated EmployeeProject entries
            var hasAssociations = await employeeProjectRepository.HasAssociationsWithProjectAsync(projectId);
            if (hasAssociations)
            {
                // Delete related entries in EmployeeProject mapping table
                await employeeProjectRepository.DeleteByProjectIdAsync(projectId);
            }

            // Delete the Project
            var project = await projectRepository.GetProjectByIdAsync(projectId);
            if (project != null)
            {
                await projectRepository.DeleteProjectAsync(projectId);
            }
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

        public async Task<IEnumerable<ProjectSelectViewModel>> GetAllProjectsForSelectAsync()
        {
            IEnumerable<Project> allProjects = await projectRepository.GetAllProjectsAsync();
            IEnumerable<ProjectSelectViewModel> model = allProjects.Select(p => new ProjectSelectViewModel
            {
                Id = p.Id,
                Name = p.Name
            });

            return model;
        }

        public async Task<ProjectDetailViewModel> GetProjectDetailsAsync(Guid projectId)
        {
            var project = await projectRepository.GetProjectWithEmployeesByIdAsync(projectId);

            if (project == null) return null;

            return new ProjectDetailViewModel
            {
                Id = projectId,
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

        public async Task<ProjectEditViewModel> GetProjectForEditAsync(Guid id)
        {
            Project project = await projectRepository.GetProjectByIdAsync(id);
            ProjectEditViewModel model = new()
            {
                Name = project.Name,
                Description = project.Description,
                Start = project.StartDate,
                End = project.EndDate
            };
            return model;
        }

        public async Task UpdateProjectAsync(ProjectEditViewModel model, Guid id)
        {
            await projectRepository.UpdateProjectAsync(model, id);
        }
    }
}
