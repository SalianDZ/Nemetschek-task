using System.ComponentModel.DataAnnotations;

namespace Hierarchy.Data.Models
{
    public class EmployeeProject
    {
        [Key]
        public Guid EmployeeID { get; set; }
        public Employee Employee { get; set; } = null!;

        [Key]
        public Guid ProjectID { get; set; }
        public Project Project { get; set; } = null!;
    }
}