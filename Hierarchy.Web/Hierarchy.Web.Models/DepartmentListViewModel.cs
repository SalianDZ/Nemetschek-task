﻿namespace Hierarchy.Web.Models
{
    public class DepartmentListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int EmployeeCount { get; set; }
    }
}
