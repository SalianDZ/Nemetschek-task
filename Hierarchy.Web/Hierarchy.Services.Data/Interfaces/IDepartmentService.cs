﻿using Hierarchy.Web.Models.Department;

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
        Task<bool> DoesDepartmentHaveAnyEmployeesAsync(Guid id);
        Task<DepartmentFormViewModel> GetDepartmentForEditAsync(Guid id);
        Task UpdateDepartmentAsync(DepartmentFormViewModel model, Guid id);
	}
}
