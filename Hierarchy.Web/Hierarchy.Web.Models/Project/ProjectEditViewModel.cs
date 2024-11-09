using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

namespace Hierarchy.Web.Models.Project
{
    public class ProjectEditViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime? End { get; set; }
    }
}
