using Hierarchy.Web.Models.Department;

namespace Hierarchy.Services.Data.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentListViewModel>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(DepartmentFormViewModel departmentViewModel);
		Task<DepartmentDetailViewModel> GetDepartmentDetailsAsync(Guid departmentId);
        Task<bool> DoesDepartmentExistAsync(string name);
        Task<IEnumerable<DepartmentSelectViewModel>> GetAllDepartmentsForSelectAsync();
        Task DeleteDepartmentAsync(Guid departmentId);
	}
}
