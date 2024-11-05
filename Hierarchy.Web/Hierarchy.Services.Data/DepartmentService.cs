using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models;

namespace Hierarchy.Services.Data
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
                this.departmentRepository = departmentRepository;
        }

        public async Task AddDepartmentAsync(DepartmentFormViewModel departmentViewModel)
        {
            Department department = new();
            department.Name = departmentViewModel.Name;
            department.Description = departmentViewModel.Description;
            await departmentRepository.AddDepartmentAsync(department);
        }

        public async Task<IEnumerable<DepartmentListViewModel>> GetAllDepartmentsAsync()
        {
            var departments = await departmentRepository.GetAllDepartmentsWithEmployeesAsync();

            // Map to DepartmentListViewModel with EmployeeCount
            return departments.Select(d => new DepartmentListViewModel
            {
                Id = d.Id,
                Name = d.Name,
                EmployeeCount = d.Employees?.Count() ?? 0
            }).ToList();
        }


    }
}
