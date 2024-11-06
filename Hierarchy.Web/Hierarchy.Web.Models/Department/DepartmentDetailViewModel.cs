using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Web.Models.Department
{
    public class DepartmentDetailViewModel
    {
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public IEnumerable<EmployeeViewModel> Employees { get; set; } = null!;

	}
}
