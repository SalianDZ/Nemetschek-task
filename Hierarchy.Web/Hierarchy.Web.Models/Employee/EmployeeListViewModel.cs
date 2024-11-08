namespace Hierarchy.Web.Models.Employee
{
    public class EmployeeListViewModel
    {
        public Guid EmployeeID { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Supervisor { get; set; } = null!;
    }
}
