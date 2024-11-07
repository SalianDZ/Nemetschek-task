using Hierarchy.Data.Models;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Department;
using Hierarchy.Web.Models.Employee;

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

        public async Task<bool> DoesDepartmentExistAsync(string name)
        {
            return await departmentRepository.DoesDepartmentExistAsync(name);
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
