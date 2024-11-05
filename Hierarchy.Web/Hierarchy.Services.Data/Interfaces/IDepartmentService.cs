using Hierarchy.Web.Models;

namespace Hierarchy.Services.Data.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentListViewModel>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(DepartmentFormViewModel departmentViewModel);
    }
}
