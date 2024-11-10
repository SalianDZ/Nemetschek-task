using Hierarchy.Web.Models.Project;

namespace Hierarchy.Services.Data.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectListViewModel>> GetAllProjectsAsync();
        Task<ProjectDetailViewModel> GetProjectDetailsAsync(Guid projectId);
        Task AddProjectAsync(ProjectFormViewModel projectViewModel);
        Task<bool> DoesProjectExistAsync(string name);
        Task<bool> DoesProjectExistByIdAsync(string id);
        Task DeleteProjectAsync(Guid projectId);
        Task<ProjectEditViewModel> GetProjectForEditAsync(Guid id);
        Task UpdateProjectAsync(ProjectEditViewModel model, Guid id);
        Task<IEnumerable<ProjectSelectViewModel>> GetAllProjectsForSelectAsync();
        Task AssignProjectToEmployeeAsync(Guid employeeId, Guid projectId);
        Task<bool> IsProjectAlreadyAssignedToEmployeeByIdAsync(Guid employeeId, Guid projectId);
    }
}
