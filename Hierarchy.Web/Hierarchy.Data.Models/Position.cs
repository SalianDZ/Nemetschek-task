using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

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
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int Rank { get; set; }

        [Required]
        public bool IsSupervisor { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}