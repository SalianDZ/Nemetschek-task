using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Services.Data.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListViewModel>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(EmployeeFormViewModel employeeForm);
        Task<IEnumerable<SupervisorSelectViewModel>> GetAllSupervisorsForSelectAsync();
        Task<IEnumerable<GenderOptions>> GetGenderOptions();
        Task<bool> DoesEmployeeExistByNameAsync(string name);
        Task DeleteEmployeeAsync(Guid employeeId);
        Task<EmployeeDetailViewModel> GetEmployeeDetailsAsync(Guid employeeId);
        Task<bool> DoesDepartmentHaveAnyEmployeesAsync(Guid departmentId);
        Task<EmployeeFormViewModel> GetEmployeeForEditAsync(Guid id);
        Task UpdateEmployeeAsync(EmployeeFormViewModel model, Guid id);
    }
}
