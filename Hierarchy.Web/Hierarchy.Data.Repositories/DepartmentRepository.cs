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

        public Task AddDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDepartmentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid id)
        {
            return await context.Departments.FindAsync(id);
        }

        public Task UpdateDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
