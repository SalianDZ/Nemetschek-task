using Hierarchy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Data
{
    public class HierarchyDbContext : DbContext
    {
        public HierarchyDbContext(DbContextOptions<HierarchyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<EmployeeProject> EmployeeProjects { get; set; } = null!;

        public DbSet<Project> Projects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeID, ep.ProjectID });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeID);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
