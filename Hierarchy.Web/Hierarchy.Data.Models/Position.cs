using System.ComponentModel.DataAnnotations;

namespace Hierarchy.Data.Models
{
    public class Position
    {
        public Position()
        {
            Id = Guid.NewGuid(); 
            Employees = new HashSet<Employee>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Rank { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}