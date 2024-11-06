using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

namespace Hierarchy.Web.Models.Department
{
    public class DepartmentFormViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Description { get; set; } = null!;
    }
}
