using Hierarchy.Data.Models;
using Hierarchy.Web.Models.Project;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(ProjectEditViewModel model, Guid id);
        Task DeleteProjectAsync(Guid id);
        Task<IEnumerable<Project>> GetAllProjectsWithEmployeesAsync();
        Task<Project> GetProjectWithEmployeesByIdAsync(Guid projectId);
        Task<bool> DoesProjectExistAsync(string name);
        Task<bool> DoesProjectExistByIdAsync(string id);
        Task AssignProjectToEmployee(Guid employeeId, Guid projectId);
    }
}
