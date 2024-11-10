using Hierarchy.Web.Models.Project;

namespace Hierarchy.Web.Models.Employee
{
    public class EmployeeDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Supervisor { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public string Gender { get; set; } = null!;
        public IEnumerable<EmployeeViewModel> Subordinates { get; set; } = null!;
        public IEnumerable<ProjectViewModel> Projects { get; set; } = null!;
    }
}
