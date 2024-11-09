using Hierarchy.Data.Models;
using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Employee>> GetAllSupervisorsAsync();
        Task<Employee> GetEmployeeWithDetailsByIdAsync(Guid employeeId);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(EmployeeFormViewModel model, Guid id);
        Task DeleteEmployeeAsync(Guid id);
        Task<bool> DoesEmployeeExistByNameAsync(string name);
        Task<bool> HasEmployeesInDepartmentAsync(Guid departmentId);
        Task<bool> HasEmployeesInPositionAsync(Guid positionId);
    }
}
