using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hierarchy.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            Id = Guid.NewGuid();    
            Subordinates = new HashSet<Employee>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int ExperienceYears { get; set; }

        [ForeignKey("Position")]
        public int PositionID { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        [ForeignKey("Supervisor")]
        public int? SupervisorID { get; set; }

        public Position Position { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public Employee Supervisor { get; set; } = null!;
        public ICollection<Employee> Subordinates { get; set; }

    }
}
