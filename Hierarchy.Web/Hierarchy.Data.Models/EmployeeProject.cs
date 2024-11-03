using System.ComponentModel.DataAnnotations;

namespace Hierarchy.Data.Models
{
    public class EmployeeProject
    {
        [Key]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; } = null!;

        [Key]
        public int ProjectID { get; set; }
        public Project Project { get; set; } = null!;
    }
}