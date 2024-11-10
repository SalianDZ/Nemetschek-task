using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Web.Models.Position
{
	public class PositionDetailViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
		public string Rank { get; set; } = null!;
		public bool IsSupervisor { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; } = null!;
	}
}
