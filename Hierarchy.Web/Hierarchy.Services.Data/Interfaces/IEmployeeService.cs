using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Services.Data.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListViewModel>> GetAllEmployeesAsync();
        //Task<EmployeeDetailViewModel> GetEmployeeDetailsAsync(Guid employeeId);
        Task AddEmployeeAsync(EmployeeFormViewModel employeeForm);
        Task<IEnumerable<SupervisorSelectViewModel>> GetAllSupervisorsForSelectAsync();
        Task<IEnumerable<GenderOptions>> GetGenderOptions();
        Task<bool> DoesEmployeeExistByNameAsync(string name);
        Task DeleteEmployeeAsync(Guid employeeId);
        Task<EmployeeDetailViewModel> GetEmployeeDetailsAsync(Guid employeeId);
        Task<bool> DoesDepartmentHaveAnyEmployeesAsync(Guid departmentId);
    }
}
