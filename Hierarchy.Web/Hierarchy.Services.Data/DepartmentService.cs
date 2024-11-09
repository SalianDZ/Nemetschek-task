using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Department;
using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Services.Data
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IEmployeeRepository employeeRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
                this.departmentRepository = departmentRepository;
                this.employeeRepository = employeeRepository;
        }

        public async Task AddDepartmentAsync(DepartmentFormViewModel departmentViewModel)
        {
            Department department = new();
            department.Name = departmentViewModel.Name;
            department.Description = departmentViewModel.Description;
            await departmentRepository.AddDepartmentAsync(department);
        }

        public async Task DeleteDepartmentAsync(Guid departmentId)
        {
            var hasEmployees = await employeeRepository.HasEmployeesInDepartmentAsync(departmentId);

            if (hasEmployees)
            {
                throw new InvalidOperationException("Cannot delete department with associated employees.");
            }

            await departmentRepository.DeleteDepartmentAsync(departmentId);
        }

        public async Task<bool> DoesDepartmentExistAsync(string name)
        {
            return await departmentRepository.DoesDepartmentExistAsync(name);
        }

        public async Task<bool> DoesDepartmentHaveAnyEmployeesAsync(Guid id)
        {
            return await departmentRepository.DoesDepartmentHaveAnyEmployeesAsync(id);
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

        public async Task<IEnumerable<DepartmentSelectViewModel>> GetAllDepartmentsForSelectAsync()
        {
            IEnumerable<Department> allDepartments = await departmentRepository.GetAllDepartmentsAsync();
            return allDepartments.Select(d => new DepartmentSelectViewModel 
            {
                Id= d.Id,
                Name = d.Name
            }).ToList();
        }

        public async Task<DepartmentDetailViewModel> GetDepartmentDetailsAsync(Guid departmentId)
		{
			var department = await departmentRepository.GetDepartmentWithEmployeesByIdAsync(departmentId);

			if (department == null)
			{
				return new DepartmentDetailViewModel
				{
					Name = "Department Not Found",
					Description = "The requested department does not exist.",
					Employees = new List<EmployeeViewModel>() // Empty list for employees
				};
			}

			// Map to DepartmentDetailViewModel
			return new DepartmentDetailViewModel
			{
				Name = department.Name,
				Description = department.Description,
				Employees = department.Employees?.Select(e => new EmployeeViewModel
				{
					EmployeeName = e.Name,
					Position = e.Position?.Name, // Assuming Position is a navigation property in Employee
					Department = e.Department?.Name
				}).ToList()
			};
		}
	}
}
