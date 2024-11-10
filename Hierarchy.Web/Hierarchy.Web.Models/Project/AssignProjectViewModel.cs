using Hierarchy.Web.Models.Employee;
using System.ComponentModel.DataAnnotations;

namespace Hierarchy.Web.Models.Project
{
    public class AssignProjectViewModel
    {
        public AssignProjectViewModel()
        {
            Employees = new HashSet<EmployeeSelectViewModel>();
            Projects = new HashSet<ProjectSelectViewModel>();
        }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public IEnumerable<EmployeeSelectViewModel> Employees { get; set; }
        public IEnumerable<ProjectSelectViewModel> Projects { get; set; }
    }
}
