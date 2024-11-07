using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly HierarchyDbContext context;

        public ProjectRepository(HierarchyDbContext context)
        {
            this.context = context;
        }

        public async Task AddProjectAsync(Project project)
        {
            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await GetProjectByIdAsync(id);
            if (project != null)
            {
                context.Projects.Remove(project);
                await context.SaveChangesAsync();
            }
        }

        public Task<bool> DoesProjectExistAsync(string name)
        {
            return context.Projects.AnyAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsWithEmployeesAsync()
        {
            return await context.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee) // Load employees through the EmployeeProjects relationship
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await context.Projects.FindAsync(id);
        }

        public async Task<Project> GetProjectWithEmployeesByIdAsync(Guid projectId)
        {
            return await context.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            context.Projects.Update(project);
            await context.SaveChangesAsync();
        }
    }
}
