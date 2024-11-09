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

        public async Task<IEnumerable<Department>> GetAllDepartmentsWithEmployeesAsync()
        {
            return await context.Departments
                .Include(d => d.Employees)
                .ToListAsync();
        }

		public async Task<Department> GetDepartmentWithEmployeesByIdAsync(Guid departmentId)
		{
			return await context.Departments
				.Include(d => d.Employees) // Eager load the Employees navigation property
				.ThenInclude(e => e.Position) // Optionally load Position details for each Employee, if available
				.FirstOrDefaultAsync(d => d.Id == departmentId);
		}

        public Task<bool> DoesDepartmentExistAsync(string name)
        {
            return context.Departments.AnyAsync(d => d.Name == name);
        }

        public async Task<bool> DoesDepartmentHaveAnyEmployeesAsync(Guid id)
        {
            Department department = await context.Departments.FirstAsync(d => d.Id == id);
            return department.Employees.Any();
        }
    }
}
