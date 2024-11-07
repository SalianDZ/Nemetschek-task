using Hierarchy.Web.Models.Employee;

namespace Hierarchy.Web.Models.Project
{
    public class ProjectDetailViewModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; } = null!;
    }
}
