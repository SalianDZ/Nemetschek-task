﻿using Hierarchy.Data.Models;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid id);
        Task<IEnumerable<Project>> GetAllProjectsWithEmployeesAsync();
        Task<Project> GetProjectWithEmployeesByIdAsync(Guid projectId);
        Task<bool> DoesProjectExistAsync(string name);
        Task<bool> DoesProjectExistByIdAsync(string id);
    }
}
