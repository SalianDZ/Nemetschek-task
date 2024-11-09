using Hierarchy.Data.Models;
using Hierarchy.Web.Models.Department;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentByIdAsync(Guid id);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(DepartmentFormViewModel model, Guid id);
        Task DeleteDepartmentAsync(Guid id);
        Task<IEnumerable<Department>> GetAllDepartmentsWithEmployeesAsync();
        Task<Department> GetDepartmentWithEmployeesByIdAsync(Guid id);
        Task<bool> DoesDepartmentExistAsync(string name);
        Task<bool> DoesDepartmentHaveAnyEmployeesAsync(Guid id);

	}
}
