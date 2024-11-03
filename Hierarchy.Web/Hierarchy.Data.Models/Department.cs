using System.ComponentModel.DataAnnotations;

namespace Hierarchy.Data.Models
{
    public class Department
    {
        public Department()
        {
            Id = Guid.NewGuid();
            Employees = new HashSet<Employee>();
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; }
    }
}