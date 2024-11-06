using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

namespace Hierarchy.Web.Models.Position
{
	public class PositionFormViewModel
	{
		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[Range(RankMinValue, RankMaxValue)]
		public int Rank { get; set; }

		[Required]
		public bool IsSupervisor { get; set; }
	}
}
