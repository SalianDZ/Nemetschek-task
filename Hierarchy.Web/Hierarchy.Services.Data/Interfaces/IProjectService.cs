﻿using Hierarchy.Web.Models.Project;

namespace Hierarchy.Services.Data.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectListViewModel>> GetAllProjectsAsync();
        Task<ProjectDetailViewModel> GetProjectDetailsAsync(Guid projectId);
        Task AddProjectAsync(ProjectFormViewModel projectViewModel);
        Task<bool> DoesProjectExistAsync(string name);
        Task<bool> DoesProjectExistByIdAsync(string id);
    }
}
