using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

namespace Hierarchy.Web.Models.Project
{
    public class ProjectFormViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;
    }
}
