using Hierarchy.Data.Models;
using Hierarchy.Data.Models.Enums;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Services.Data
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task AddEmployeeAsync(EmployeeFormViewModel employeeForm)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = employeeForm.Name,
                Gender = (Gender) employeeForm.Gender,
                ExperienceYears = employeeForm.ExperienceYears,
                PositionID = employeeForm.PositionId,
                DepartmentID = employeeForm.DepartmentId,
                SupervisorID = employeeForm.SupervisorId
            };

            await employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<IEnumerable<EmployeeListViewModel>> GetAllEmployeesAsync()
        {
            var employees = await employeeRepository.GetAllEmployeesAsync();

            return employees.Select(e => new EmployeeListViewModel
            {
                EmployeeID = e.Id,
                Name = e.Name,
                Position = e.Position.Name,
                Department = e.Department.Name,
                Supervisor = e.Supervisor?.Name ?? "None"
            }).ToList();
        }

        public async Task<IEnumerable<SupervisorSelectViewModel>> GetAllSupervisorsForSelectAsync()
        {
            IEnumerable<Employee> allSupervisors = await employeeRepository.GetAllSupervisorsAsync();
            return allSupervisors.Select(s => new SupervisorSelectViewModel
            { 
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public async Task<IEnumerable<GenderOptions>> GetGenderOptions()
        {
                return new List<GenderOptions>
            {
                new GenderOptions { Value = 1, Text = "Male" },
                new GenderOptions { Value = 2, Text = "Female" },
                new GenderOptions { Value = 3, Text = "NonBinary" },
                new GenderOptions { Value = 4, Text = "Other" }
            };
        }
    }
}
