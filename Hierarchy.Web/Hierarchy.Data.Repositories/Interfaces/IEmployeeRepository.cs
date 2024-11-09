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
        Task<bool> DoesEmployeeExistByNameAsync(string name);
        Task<bool> HasEmployeesInDepartmentAsync(Guid departmentId);
        Task<bool> HasEmployeesInPositionAsync(Guid positionId);
    }
}
