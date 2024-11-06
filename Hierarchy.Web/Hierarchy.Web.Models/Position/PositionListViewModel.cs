namespace Hierarchy.Web.Models.Position
{
	public class PositionListViewModel
	{
		public Guid PositionID { get; set; }
		public string Name { get; set; } = null!;
		public int EmployeeCount { get; set; }
	}
}
