using Hierarchy.Data.Models;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Employee>> GetAllSupervisorsAsync();
        Task<Employee> GetEmployeeWithDetailsByIdAsync(Guid employeeId);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid id);
    }
}
