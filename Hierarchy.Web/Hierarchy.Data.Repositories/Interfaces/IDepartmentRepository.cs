using Hierarchy.Data.Models;

namespace Hierarchy.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentByIdAsync(Guid id);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Guid id);
    }
}
