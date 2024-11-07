namespace Hierarchy.Web.Models.Project
{
    public class ProjectListViewModel
    {
        public Guid ProjectID { get; set; }
        public string Name { get; set; } = null!;
        public int EmployeeCount { get; set; }
    }
}
