using Hierarchy.Data.Models;
using Hierarchy.Data.Models.Enums;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Web.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HierarchyDbContext context;

        public EmployeeRepository(HierarchyDbContext context)
        {
                this.context = context;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await GetEmployeeByIdAsync(id);

            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> DoesEmployeeExistByNameAsync(string name)
        {
            return await context.Employees.AnyAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.Supervisor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllSupervisorsAsync()
        {
            return await context.Employees.Include(e => e.Supervisor).Where(e => e.SupervisorID == null).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await context.Employees
                .Include(e => e.Department)   // Include Department for navigation property
                .Include(e => e.Position)     // Include Position for navigation property
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> GetEmployeeWithDetailsByIdAsync(Guid employeeId)
        {
            return await context.Employees
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.Supervisor)
                .Include(e => e.Subordinates)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<bool> HasEmployeesInDepartmentAsync(Guid departmentId)
        {
            return await context.Employees.AnyAsync(e => e.DepartmentID == departmentId);
        }

        public async Task<bool> HasEmployeesInPositionAsync(Guid positionId)
        {
            return await context.Employees.AnyAsync(e => e.PositionID == positionId);
        }

        public async Task UpdateEmployeeAsync(EmployeeFormViewModel model, Guid id)
        {
            Employee employee = await GetEmployeeByIdAsync(id);
            employee.Name = model.Name;
            employee.Gender = (Gender) model.Gender;
            employee.ExperienceYears = model.ExperienceYears;
            employee.SupervisorID = model.IsSupervisor ? null : model.SupervisorId;
            employee.DepartmentID = model.DepartmentId;
            employee.PositionID = model.PositionId;
            await context.SaveChangesAsync();
        }
    }
}
