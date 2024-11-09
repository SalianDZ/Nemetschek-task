using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data.Repositories
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly HierarchyDbContext context;

        public EmployeeProjectRepository(HierarchyDbContext context)
        {
            this.context = context;
        }

        public async Task DeleteByProjectIdAsync(Guid projectId)
        {
            var associations = context.EmployeeProjects.Where(ep => ep.ProjectID == projectId);
            context.EmployeeProjects.RemoveRange(associations);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetEmployeeProjectsByEmployeeIdAsync(Guid employeeId)
        {
            return await context.Set<EmployeeProject>()
                .Where(ep => ep.EmployeeID == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetEmployeeProjectsByProjectIdAsync(Guid projectId)
        {
            return await context.Set<EmployeeProject>()
                .Where(ep => ep.ProjectID == projectId)
                .ToListAsync();
        }

        public async Task<bool> HasAssociationsWithProjectAsync(Guid projectId)
        {
            return await context.EmployeeProjects.AnyAsync(ep => ep.ProjectID == projectId);
        }
    }
}
