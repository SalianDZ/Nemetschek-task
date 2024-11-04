using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HierarchyDbContext context;

        public DepartmentRepository(HierarchyDbContext context)
        {
            this.context = context;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(Guid id)
        {
            var department = await GetDepartmentByIdAsync(id);
            if (department != null)
            {
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid id)
        {
            return await context.Departments.FindAsync(id);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            context.Departments.Update(department);
            await context.SaveChangesAsync();
        }
    }
}
