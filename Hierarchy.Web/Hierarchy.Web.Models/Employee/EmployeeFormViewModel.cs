using Hierarchy.Web.Models.Department;
using Hierarchy.Web.Models.Position;
using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

namespace Hierarchy.Web.Models.Employee
{
    public class EmployeeFormViewModel
    {
        public EmployeeFormViewModel()
        {
            Positions = new HashSet<PositionSelectViewModel>();
            Departments = new HashSet<DepartmentSelectViewModel>();
            Supervisors = new HashSet<SupervisorSelectViewModel>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]  
        public int Gender { get; set; }

        [Required]
        public int ExperienceYears { get; set; }

        [Required]
        [Display(Name = "Position")]
        public Guid PositionId { get; set; }

        public IEnumerable<PositionSelectViewModel> Positions { get; set; }

        [Required]
        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }

        public IEnumerable<DepartmentSelectViewModel> Departments { get; set; }

        [Required]
        [Display(Name = "Supervisor")]
        public Guid? SupervisorId { get; set; }

        public IEnumerable<SupervisorSelectViewModel> Supervisors { get; set; }
    }
}
